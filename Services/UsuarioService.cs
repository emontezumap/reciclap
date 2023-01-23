using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using Contenedores;
using Filtros;
using Herramientas;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Validadores;

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

    public async Task<Usuario> CrearUsuario(UsuarioDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));
            ValidadorUsuario vc = new ValidadorUsuario(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Usuario usr = new Usuario()
                {
                    Activo = nuevo.Activo,
                    Apellido = nuevo.Apellido!,
                    Apellido2 = nuevo.Apellido2 == null ? "" : nuevo.Apellido2!,
                    Clave = Cripto.CodigoSHA256(nuevo.Clave!),
                    Direccion = nuevo.Direccion!,
                    Email = nuevo.Email!,
                    Email2 = nuevo.Email2 == null ? "" : nuevo.Email2!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCiudad = (Guid)nuevo.IdCiudad!,
                    IdCreador = id,
                    IdGrupo = (Guid)nuevo.IdGrupo!,
                    IdModificador = id,
                    IdProfesion = nuevo.IdProfesion,
                    MaximoPublicaciones = nuevo.MaximoPublicaciones == null ? 0 : (int)nuevo.MaximoPublicaciones!,
                    Nombre = nuevo.Nombre!,
                    Nombre2 = nuevo.Nombre2 == null ? "" : nuevo.Nombre2!,
                    Perfil = nuevo.Perfil == null ? "" : nuevo.Perfil!,
                    Telefono = nuevo.Telefono!,
                    Telefono2 = nuevo.Telefono2 == null ? "" : nuevo.Telefono2
                };

                ctx.Usuarios.Add(usr);
                await ctx.SaveChangesAsync();

                return usr;
            }
            else
                throw (new Excepcionador(rv).ExcepcionDatosNoValidos());
        }
    }

    public async Task<bool> ModificarUsuario(UsuarioDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorUsuario vc = new ValidadorUsuario(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Usuarios.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

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
                    buscado.IdModificador = id;
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
            else
                throw (new Excepcionador(rv).ExcepcionDatosNoValidos());
        }

        return false;
    }

    public async Task<bool> EliminarUsuario(Guid id, ClaimsPrincipal claims)
    {
        UsuarioDTO usr = new UsuarioDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarUsuario(usr, claims);

    }
}