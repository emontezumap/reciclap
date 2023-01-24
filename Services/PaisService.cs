using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class PaisService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public PaisService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Pais>> TodosLosPaises()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Paises.ToListAsync<Pais>();
        }
    }

    public async Task<Pais?> UnPais(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Paises.FindAsync(id);
        }
    }

    public async Task<Pais> CrearPais(PaisDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPais vc = new ValidadorPais(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Pais pais = new Pais()
                {
                    Activo = nuevo.Activo,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdModificador = id,
                    Nombre = nuevo.Nombre!
                };

                ctx.Paises.Add(pais);
                await ctx.SaveChangesAsync();

                return pais;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarPais(PaisDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPais vc = new ValidadorPais(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Paises.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;
                    buscado.IdModificador = id;
                    buscado.FechaModificacion = DateTime.UtcNow;

                    await ctx.SaveChangesAsync();
                    return true;
                }
                else
                    throw (new Excepcionador()).ExcepcionRegistroEliminado();
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> EliminarPais(Guid id, ClaimsPrincipal claims)
    {
        PaisDTO pais = new PaisDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarPais(pais, claims);
    }
}