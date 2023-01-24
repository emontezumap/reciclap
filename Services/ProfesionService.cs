using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class ProfesionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ProfesionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Profesion>> TodasLasProfesiones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Profesiones.ToListAsync<Profesion>();
        }
    }

    public async Task<Profesion?> UnaProfesion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Profesiones.FindAsync(id);
        }
    }

    public async Task<Profesion> CrearProfesion(ProfesionDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorProfesion vc = new ValidadorProfesion(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                Profesion prof = new Profesion()
                {
                    Activo = nuevo.Activo,
                    Descripcion = nuevo.Descripcion!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdModificador = id
                };

                ctx.Profesiones.Add(prof);
                await ctx.SaveChangesAsync();
                return prof;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarProfesion(ProfesionDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorProfesion vc = new ValidadorProfesion(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Profesiones.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdModificador = id;

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

    public async Task<bool> EliminarProfesion(Guid id, ClaimsPrincipal claims)
    {
        ProfesionDTO prof = new ProfesionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarProfesion(prof, claims);
    }
}