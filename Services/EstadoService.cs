using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class EstadoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public EstadoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Estado>> TodosLosEstados()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.ToListAsync<Estado>();
        }
    }

    public async Task<Estado?> UnEstado(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.FindAsync(id);
        }
    }

    public async Task<Estado> CrearEstado(EstadoDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorEstado vc = new ValidadorEstado(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Estado est = new Estado()
                {
                    Activo = nuevo.Activo,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdModificador = id,
                    IdPais = (Guid)nuevo.IdPais!,
                    Nombre = nuevo.Nombre!
                };

                ctx.Estados.Add(est);
                await ctx.SaveChangesAsync();

                return est;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarEstado(EstadoDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorEstado vc = new ValidadorEstado(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Estados.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdModificador = id;
                    buscado.IdPais = modif.IdPais == null ? buscado.IdPais : (Guid)modif.IdPais;
                    buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;

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

    public async Task<bool> EliminarEstado(Guid id, ClaimsPrincipal claims)
    {
        EstadoDTO est = new EstadoDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarEstado(est, claims);
    }
}