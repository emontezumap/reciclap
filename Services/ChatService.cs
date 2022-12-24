using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class ChatService
{
    private readonly SSDBContext ctx;

    public ChatService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Chat>> Todos()
    {
        return await ctx.Chats.ToListAsync<Chat>();
    }

    public async Task<Chat?> PorId(Guid id)
    {
        return await ctx.Chats.FindAsync(id);
    }

    public async Task<Chat> Crear(Chat nuevo)
    {
        ctx.Chats.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Chat modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Titulo = modif.Titulo;
            buscado.Fecha = modif.Fecha;
            buscado.IdPublicacion = modif.IdPublicacion;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Chats.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}