using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class PersonalService
{
    private readonly SSDBContext ctx;

    public PersonalService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Personal>> Todos()
    {
        return await ctx.Personal.ToListAsync<Personal>();
    }

    public async Task<Personal?> PorId(Guid idPub, Guid idUsr)
    {
        return await ctx.Personal.FindAsync(idPub, idUsr);
    }

    public async Task<Personal> Crear(Personal nuevo)
    {
        ctx.Personal.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Personal modif)
    {
        var buscado = await PorId(modif.IdPublicacion, modif.IdUsuario);

        if (buscado != null)
        {
            buscado.Fecha = modif.Fecha;
            buscado.IdRol = modif.IdRol;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid idPub, Guid idUsr)
    {
        var buscado = await PorId(idPub, idUsr);

        if (buscado != null)
        {
            ctx.Personal.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}