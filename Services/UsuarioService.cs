using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using Contenedores;
using Filtros;
using Herramientas;

namespace Services;

[ExtendObjectType("Mutacion")]
public class UsuarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public UsuarioService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Usuario>> TodosLosUsuarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Usuarios.ToListAsync<Usuario>();
        }
    }

    public async Task<Usuario?> UnUsuario(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Usuarios.FindAsync(id);
        }
    }

    public async Task<Usuario> CrearUsuario(UsuarioDTO nuevo)
    {
        Usuario usr = new Usuario()
        {
            Activo = nuevo.Activo,
            Apellido = nuevo.Apellido,
            Apellido2 = nuevo.Apellido2,
            Clave = Cripto.CodigoSHA256(nuevo.Clave),
            Direccion = nuevo.Direccion,
            Email = nuevo.Email,
            Email2 = nuevo.Email2,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IdCiudad = (Guid)nuevo.IdCiudad,
            // IdCreador = 
            IdGrupo = (Guid)nuevo.IdGrupo,
            // IdModificador = 
            IdProfesion = nuevo.IdProfesion,
            MaximoPublicaciones = (int)nuevo.MaximoPublicaciones,
            Nombre = nuevo.Nombre,
            Nombre2 = nuevo.Nombre2,
            Perfil = nuevo.Perfil,
            Telefono = nuevo.Telefono,
            Telefono2 = nuevo.Telefono2
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Usuarios.Add(usr);
            await ctx.SaveChangesAsync();
        }

        return usr;
    }

    public async Task<bool> ModificarUsuario(UsuarioDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Usuarios.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.Apellido = modif.Apellido == null ? buscado.Apellido : modif.Apellido;
                buscado.Apellido2 = modif.Apellido2 == null ? buscado.Apellido2 : modif.Apellido2;
                buscado.Clave = modif.Clave == null ? buscado.Clave : Cripto.CodigoSHA256(modif.Clave);
                buscado.Direccion = modif.Direccion == null ? buscado.Direccion : modif.Direccion;
                buscado.Email = modif.Email == null ? buscado.Email : modif.Email;
                buscado.Email2 = modif.Email2 == null ? buscado.Email2 : modif.Email2;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.IdCiudad = modif.IdCiudad == null ? buscado.IdCiudad : (Guid)modif.IdCiudad;
                buscado.IdGrupo = modif.IdGrupo == null ? buscado.IdGrupo : (Guid)modif.IdGrupo;
                // buscado.IdModificador = 
                buscado.IdProfesion = modif.IdProfesion == null ? buscado.IdProfesion : modif.IdProfesion;
                buscado.MaximoPublicaciones = modif.MaximoPublicaciones == null ? buscado.MaximoPublicaciones : (int)modif.MaximoPublicaciones;
                buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;
                buscado.Nombre2 = modif.Nombre2 == null ? buscado.Nombre2 : modif.Nombre2;
                buscado.Perfil = modif.Perfil == null ? buscado.Perfil : modif.Perfil;
                buscado.Telefono = modif.Telefono == null ? buscado.Telefono : modif.Telefono;
                buscado.Telefono2 = modif.Telefono2 == null ? buscado.Telefono2 : modif.Telefono2;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarUsuario(Guid id)
    {
        UsuarioDTO usr = new UsuarioDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarUsuario(usr);

    }
}