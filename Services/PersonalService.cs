using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

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

    public async Task<Personal> CrearPersonal(PersonalDTO nuevo)
    {
        Personal per = new Personal()
        {
            Activo = nuevo.Activo,
            Fecha = (DateTime)nuevo.Fecha,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            // IdCreador = 
            // IdModificador = 
            IdPublicacion = (Guid)nuevo.IdPublicacion,
            IdRol = (Guid)nuevo.IdRol,
            IdUsuario = (Guid)nuevo.IdUsuario,
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Personal.Add(per);
            await ctx.SaveChangesAsync();
        }

        return per;
    }

    public async Task<bool> ModificarPersonal(PersonalDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Personal.FindAsync(modif.IdPublicacion, modif.IdUsuario);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                buscado.FechaModificacion = DateTime.UtcNow;
                // buscado.IdModificador = 
                buscado.IdRol = modif.IdRol == null ? buscado.IdRol : (Guid)modif.IdRol;

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarPersonal(Guid idPub, Guid idUsr)
    {
        PersonalDTO per = new PersonalDTO()
        {
            IdPublicacion = idPub,
            IdUsuario = idUsr,
            Activo = false
        };

        return await ModificarPersonal(per);
    }
}