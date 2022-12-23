using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Services;

public class UsuarioService
{
    private readonly SSDBContext ctx;

    public UsuarioService(SSDBContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Usuario>> Todos()
    {
        return await ctx.Usuarios.ToListAsync<Usuario>();
    }

    public async Task<Usuario?> PorId(Guid id)
    {
        return await ctx.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> Crear(Usuario usr)
    {
        ctx.Usuarios.Add(usr);
        await ctx.SaveChangesAsync();

        return usr;
    }

    public async Task Modificar(Usuario usr)
    {
        var buscado = await PorId(usr.Id);

        if (buscado != null)
        {
            buscado.Apellido = usr.Apellido;
            buscado.Apellido2 = usr.Apellido2;
            buscado.Direccion = usr.Direccion;
            buscado.Email = usr.Email;
            buscado.Email2 = usr.Email2;
            buscado.IdCiudad = usr.IdCiudad;
            buscado.IdProfesion = usr.IdProfesion;
            buscado.MaximoPublicaciones = usr.MaximoPublicaciones;
            buscado.Nombre = usr.Nombre;
            buscado.Nombre2 = usr.Nombre2;
            buscado.Perfil = usr.Perfil;
            buscado.Telefono = usr.Telefono;
            buscado.Telefono2 = usr.Telefono2;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            ctx.Usuarios.Remove(buscado);
            await ctx.SaveChangesAsync();
        }
    }
}