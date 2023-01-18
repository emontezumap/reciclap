using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

[ExtendObjectType("Mutacion")]
public class ComentarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ComentarioService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Comentario>> TodosLosComentarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Comentarios.ToListAsync<Comentario>();
        }
    }

    public async Task<Comentario?> UnComentario(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Comentarios.FindAsync(id);
        }
    }

    public async Task<Comentario> CrearComentario(ComentarioDTO nuevo)
    {
        Comentario com = new Comentario()
        {
            Activo = nuevo.Activo,
            Fecha = (DateTime)nuevo.Fecha,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IdChat = (Guid)nuevo.IdChat,
            IdComentario = (Guid)nuevo.IdComentario,
            // IdCreador = 
            // IdModificador = 
            IdUsuario = (Guid)nuevo.IdUsuario,
            Texto = nuevo.Texto,
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Comentarios.Add(com);
            await ctx.SaveChangesAsync();
        }

        return com;
    }

    public async Task<bool> ModificarComentario(ComentarioDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Comentarios.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                buscado.FechaCreacion = DateTime.UtcNow == null ? buscado.FechaCreacion : DateTime.UtcNow;
                buscado.FechaModificacion = buscado.FechaCreacion;
                buscado.IdChat = modif.IdChat == null ? buscado.IdChat : (Guid)modif.IdChat;
                buscado.IdComentario = modif.IdComentario == null ? buscado.IdComentario : (Guid)modif.IdComentario;
                // buscado.IdCreador = 
                buscado.IdModificador = buscado.IdCreador;
                buscado.IdUsuario = modif.IdUsuario == null ? buscado.IdUsuario : (Guid)modif.IdUsuario;
                buscado.Texto = modif.Texto == null ? buscado.Texto : modif.Texto;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarComentario(Guid id)
    {
        ComentarioDTO com = new ComentarioDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarComentario(com);
    }
}