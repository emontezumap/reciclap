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
        // string ultimoApellido = "A";
        return await ctx.Usuarios
        // .OrderBy(u => u.Apellido)
        // .ThenBy(u => u.Apellido2)
        // .Where(u => u.Apellido.CompareTo(ultimoApellido) > 0)
        // .Take(10)
        .ToListAsync<Usuario>();
    }

    public async Task<Usuario?> PorId(Guid id)
    {
        return await ctx.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> Crear(Usuario nuevo)
    {
        ctx.Usuarios.Add(nuevo);
        await ctx.SaveChangesAsync();

        return nuevo;
    }

    public async Task Modificar(Usuario modif)
    {
        var buscado = await PorId(modif.Id);

        if (buscado != null)
        {
            buscado.Apellido = modif.Apellido;
            buscado.Apellido2 = modif.Apellido2;
            buscado.Direccion = modif.Direccion;
            buscado.Email = modif.Email;
            buscado.Email2 = modif.Email2;
            buscado.IdCiudad = modif.IdCiudad;
            buscado.IdProfesion = modif.IdProfesion;
            buscado.MaximoPublicaciones = modif.MaximoPublicaciones;
            buscado.Nombre = modif.Nombre;
            buscado.Nombre2 = modif.Nombre2;
            buscado.Perfil = modif.Perfil;
            buscado.Telefono = modif.Telefono;
            buscado.Telefono2 = modif.Telefono2;
            buscado.IdModificador = modif.IdModificador;
            buscado.FechaModificacion = DateTime.UtcNow;

            await ctx.SaveChangesAsync();
        }
    }

    public async Task Eliminar(Guid id)
    {
        var buscado = await PorId(id);

        if (buscado != null)
        {
            buscado.Activo = false;
            await ctx.SaveChangesAsync();
        }
    }
}