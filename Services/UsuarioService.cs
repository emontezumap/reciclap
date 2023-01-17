using Microsoft.EntityFrameworkCore;
using Entidades;
using Contenedores;
using Filtros;

namespace Services;

[ExtendObjectType("Mutacion")]
public class UsuarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public UsuarioService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    // public async Task<IEnumerable<Usuario>> Todos(FiltroPaginacion fp, string ruta)
    public async Task<IEnumerable<Usuario>> TodosLosUsuarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Usuarios.ToListAsync<Usuario>();
        }
    }

    public async Task<Usuario?> UsuarioPorId(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Usuarios.FindAsync(id);
        }
    }

    public async Task<Usuario> CrearUsuario(Usuario nuevo)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Usuarios.Add(nuevo);
            await ctx.SaveChangesAsync();
        }

        return nuevo;
    }

    public async Task<bool> ModificarUsuario(Usuario modif)
    {
        var buscado = await UsuarioPorId(modif.Id);

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

    public async Task<bool> EliminarUsuario(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await UsuarioPorId(id);

            if (buscado != null)
            {
                buscado.Activo = false;
                return await ModificarUsuario(buscado);
            }

            return false;
        }
    }
}