using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

namespace Services;

[ExtendObjectType("Mutacion")]
public class ChatService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ChatService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Chat>> TodosLosChats()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Chats.ToListAsync<Chat>();
        }
    }

    public async Task<Chat?> UnChat(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Chats.FindAsync(id);
        }
    }

    public async Task<Chat> CrearChat(ChatDTO nuevo)
    {
        Chat chat = new Chat()
        {
            Activo = nuevo.Activo,
            Fecha = (DateTime)nuevo.Fecha,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            // IdCreador = 
            // IdModificador = 
            IdPublicacion = (Guid)nuevo.IdPublicacion,
            Titulo = nuevo.Titulo
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Chats.Add(chat);
            await ctx.SaveChangesAsync();
        }

        return chat;
    }

    public async Task<bool> ModificarChat(ChatDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Chats.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.IdPublicacion = modif.IdPublicacion == null ? buscado.IdPublicacion : (Guid)modif.IdPublicacion;
                // buscado.IdModificador = 
                buscado.Titulo = modif.Titulo == null ? buscado.Titulo : modif.Titulo;

                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

    public async Task<bool> EliminarChat(Guid id)
    {
        ChatDTO chat = new ChatDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarChat(chat);
    }
}