using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using Herramientas;

namespace Services;

public class LoginService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public LoginService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<Usuario?> BuscarUsuario(LoginDTO usuario)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var usr = await ctx.Usuarios.SingleOrDefaultAsync(u =>
                u.Email == usuario.Email &&
                u.Clave == Cripto.CodigoSHA256(usuario.Clave));

            if (usr != null)
                ctx.Entry(usr).Reference(u => u.Grupo).Load();

            return usr;
        }
    }
}
