using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

namespace Services;

public class LoginService
{
    private readonly SSDBContext ctx;

    public LoginService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Usuario?> BuscarUsuario(LoginDTO usuario)
    {
        return await ctx.Usuarios.SingleOrDefaultAsync(u =>
            u.Email == usuario.Email &&
            u.Clave == Cripto.CodigoSHA256(usuario.Clave));
    }
}