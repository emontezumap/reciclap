using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using Herramientas;
using DB;

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

    public async Task<Administrador?> BuscarAdministrador(LoginDTO admin)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var adm = await ctx.Administradores.SingleOrDefaultAsync(a =>
                a.Email == admin.Email &&
                a.Clave == Cripto.CodigoSHA256(admin.Clave!));

            if (adm != null)
                if (adm.IdGrupo != null)
                    ctx.Entry(adm).Reference(a => a.Grupo).Load();
                else
                {
                    var g = ctx.Varias.Where(v =>
                        v.Descripcion.ToLower() == "administradores")
                        .FirstOrDefault();

                    if (g != null)
                    {
                        adm.IdGrupo = g.Id;
                        ctx.Entry(adm).Reference(a => a.Grupo).Load();
                    }
                }

            return adm;
        }
    }
}
