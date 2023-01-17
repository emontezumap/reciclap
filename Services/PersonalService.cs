using Microsoft.EntityFrameworkCore;
using Entidades;

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

    public async Task<Personal?> PersonalPorId(Guid idPub, Guid idUsr)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Personal.FindAsync(idPub, idUsr);
        }
    }

    public async Task<Personal> CrearPersonal(Personal nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Personal.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarPersonal(Personal modif)
    {
        var buscado = await PersonalPorId(modif.IdPublicacion, modif.IdUsuario);

        if (buscado != null)
        {
            using (var ctx = ctxFactory.CreateDbContext())
            {
                ctx.Update(modif);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarPersonal(Guid idPub, Guid idUsr)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await PersonalPorId(idPub, idUsr);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarPersonal(buscado);
            }

            return false;
        }
    }
}