using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class PersonalService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public PersonalService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Personal>> TodoElPersonal()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Personal.ToListAsync<Personal>();
        }
    }

    public async Task<Personal?> UnaPersona(Guid idPub, Guid idUsr)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Personal.FindAsync(idPub, idUsr);
        }
    }

    public async Task<Personal> CrearPersonal(PersonalDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPersonal vc = new ValidadorPersonal(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                Personal per = new Personal()
                {
                    Activo = nuevo.Activo,
                    Fecha = (DateTime)nuevo.Fecha!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    IdCreador = id,
                    IdModificador = id,
                    IdPublicacion = (Guid)nuevo.IdPublicacion!,
                    IdRol = (Guid)nuevo.IdRol!,
                    IdUsuario = (Guid)nuevo.IdUsuario!,
                };

                ctx.Personal.Add(per);
                await ctx.SaveChangesAsync();

                return per;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarPersonal(PersonalDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPersonal vc = new ValidadorPersonal(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Personal.FindAsync(modif.IdPublicacion, modif.IdUsuario);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdModificador = id;
                    buscado.IdRol = modif.IdRol == null ? buscado.IdRol : (Guid)modif.IdRol;

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

    public async Task<bool> EliminarPersonal(Guid idPub, Guid idUsr, ClaimsPrincipal claims)
    {
        PersonalDTO per = new PersonalDTO()
        {
            IdPublicacion = idPub,
            IdUsuario = idUsr,
            Activo = false
        };

        return await ModificarPersonal(per, claims);
    }
}