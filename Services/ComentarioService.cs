using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class ComentarioService
{
    private readonly SSDBContext ctx;

    public ComentarioService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Comentario>> Todos()
    {
        return await ctx.Comentarios.ToListAsync<Comentario>();
    }

    public async Task<Comentario?> PorId(Guid id)
    {
        return await ctx.Comentarios.FindAsync(id);
    }

    public async Task<Comentario> Crear(Comentario nuevo)
    {
        ctx.Comentarios.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Comentario modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.IdChat = modif.IdChat;
            buscado.IdUsuario = modif.IdUsuario;
            buscado.Fecha = modif.Fecha;
            buscado.Texto = modif.Texto;
            buscado.IdComentario = modif.IdComentario;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Comentarios.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}