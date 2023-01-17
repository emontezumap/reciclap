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

    public async Task<Comentario?> ComentarioPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Comentarios.FindAsync(id);
        }
    }

    public async Task<Comentario> CrearComentario(Comentario nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Comentarios.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarComentario(Comentario modif)
    {
        var buscado = await ComentarioPorId(modif.Id);

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

    public async Task<bool> EliminarComentario(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ComentarioPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarComentario(buscado);
            }

            return false;
        }
    }
}