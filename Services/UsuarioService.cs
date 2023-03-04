
using Microsoft.EntityFrameworkCore;
using Entidades;
using DB;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;
using HotChocolate.Types;

namespace Services;

[ExtendObjectType("Mutacion")]

public class UsuarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public UsuarioService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<Usuario> CrearUsuario(UsuarioDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorUsuario vc = new ValidadorUsuario(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Usuario obj = new Usuario();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Usuarios.Add(obj);
                    await ctx.SaveChangesAsync();

                    return obj;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteUsuario(List<UsuarioDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorUsuario vc = new ValidadorUsuario(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Usuario obj = new Usuario();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Usuarios.Add(obj);
                }
                else
                    res.Add((nuevo.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
            }

            return res;
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
                    Mapear(buscado, modif, id, Operacion.Modificacion);

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                    }
                    catch (Exception ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                    }
                }
                else
                    throw (new Excepcionador()).ExcepcionRegistroEliminado();
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteUsuario(List<UsuarioDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Usuario> objs = new List<Usuario>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorUsuario vc = new ValidadorUsuario(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Usuario obj = new Usuario();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Usuarios.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarUsuario(Guid id, ClaimsPrincipal claims)
    {
        UsuarioDTO pub = new UsuarioDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarUsuario(pub, claims);
    }

    public void Mapear(Usuario obj, UsuarioDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.Nombre = dto.Nombre!;
			obj.Apellido = dto.Apellido!;
			obj.SegundoNombre = dto.SegundoNombre!;
			obj.SegundoApellido = dto.SegundoApellido!;
			obj.Perfil = dto.Perfil!;
			obj.Direccion = dto.Direccion!;
			obj.IdCiudad = (int?)dto.IdCiudad!;
			obj.Telefono = dto.Telefono!;
			obj.Telefono2 = dto.Telefono2!;
			obj.Email = dto.Email!;
			obj.Clave = dto.Clave!;
			obj.Email2 = dto.Email2!;
			obj.IdProfesion = (int?)dto.IdProfesion!;
			obj.MaximoPublicaciones = (int)dto.MaximoPublicaciones!;
			obj.IdGrupo = (int?)dto.IdGrupo!;
			obj.Estatus = dto.Estatus!;
			obj.IdTipoUsuario = (int)dto.IdTipoUsuario!;
			obj.IdRol = (int?)dto.IdRol!;
			obj.UltimaIp = dto.UltimaIp!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.Nombre = dto.Nombre == null ? obj.Nombre : dto.Nombre;
			obj.Apellido = dto.Apellido == null ? obj.Apellido : dto.Apellido;
			obj.SegundoNombre = dto.SegundoNombre == null ? obj.SegundoNombre : dto.SegundoNombre;
			obj.SegundoApellido = dto.SegundoApellido == null ? obj.SegundoApellido : dto.SegundoApellido;
			obj.Perfil = dto.Perfil == null ? obj.Perfil : dto.Perfil;
			obj.Direccion = dto.Direccion == null ? obj.Direccion : dto.Direccion;
			obj.IdCiudad = dto.IdCiudad == null ? obj.IdCiudad : (int?)dto.IdCiudad;
			obj.Telefono = dto.Telefono == null ? obj.Telefono : dto.Telefono;
			obj.Telefono2 = dto.Telefono2 == null ? obj.Telefono2 : dto.Telefono2;
			obj.Email = dto.Email == null ? obj.Email : dto.Email;
			obj.Clave = dto.Clave == null ? obj.Clave : dto.Clave;
			obj.Email2 = dto.Email2 == null ? obj.Email2 : dto.Email2;
			obj.IdProfesion = dto.IdProfesion == null ? obj.IdProfesion : (int?)dto.IdProfesion;
			obj.MaximoPublicaciones = dto.MaximoPublicaciones == null ? obj.MaximoPublicaciones : (int)dto.MaximoPublicaciones;
			obj.IdGrupo = dto.IdGrupo == null ? obj.IdGrupo : (int?)dto.IdGrupo;
			obj.Estatus = dto.Estatus == null ? obj.Estatus : dto.Estatus;
			obj.IdTipoUsuario = dto.IdTipoUsuario == null ? obj.IdTipoUsuario : (int)dto.IdTipoUsuario;
			obj.IdRol = dto.IdRol == null ? obj.IdRol : (int?)dto.IdRol;
			obj.UltimaIp = dto.UltimaIp == null ? obj.UltimaIp : dto.UltimaIp;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }

    public async Task<bool> EliminarLoteUsuario(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Usuario> objs = new List<Usuario>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Usuarios.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Usuarios.UpdateRange(objs);

                try
                {
                    await ctx.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
            }
            else
                return false;
        }
    }

}
