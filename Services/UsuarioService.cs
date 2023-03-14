using Microsoft.EntityFrameworkCore;
using Entidades;
using DB;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;
using FluentValidation.Results;

namespace Services;

[ExtendObjectType("Mutacion")]

public class UsuarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorUsuarioRequeridos validadorReqs;
    private readonly ValidadorUsuario validador;

    public UsuarioService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorUsuarioRequeridos validadorReqs, ValidadorUsuario validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<Guid>> CrearUsuario(List<UsuarioDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                Usuario obj = new Usuario();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.Usuarios.Add(obj);
                codigos.Add(v.Entity.Id);
            }

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> ModificarUsuario(List<UsuarioDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Usuario> objs = new List<Usuario>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Usuarios.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Creacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Usuarios.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> EliminarUsuario(List<Guid> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Usuario> objs = new List<Usuario>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var id in ids)
            {
                var buscado = await ctx.Usuarios.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            ctx.Usuarios.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    private void ValidarDatos(UsuarioDTO dto, Operacion op = Operacion.Modificacion)
    {
        ValidationResult vr;

        if (op == Operacion.Creacion)
        {
            vr = validadorReqs.Validate(dto);

            if (!vr.IsValid)
                throw new GraphQLException(vr.ToString());
        }

        vr = validador.Validate(dto);

        if (!vr.IsValid)
            throw new GraphQLException(vr.ToString());
    }

	public void Mapear(Usuario obj, UsuarioDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.Nombre = dto.Nombre!;
			obj.Apellido = dto.Apellido!;
			obj.SegundoNombre = (string?)dto.SegundoNombre!;
			obj.SegundoApellido = (string?)dto.SegundoApellido!;
			obj.Perfil = (string?)dto.Perfil!;
			obj.Direccion = dto.Direccion!;
			obj.IdCiudad = (int)dto.IdCiudad!;
			obj.Telefono = dto.Telefono!;
			obj.Telefono2 = (string?)dto.Telefono2!;
			obj.Email = dto.Email!;
			obj.Clave = Cripto.CodigoSHA256(dto.Clave!);
			obj.Email2 = (string?)dto.Email2!;
			obj.IdProfesion = (int?)dto.IdProfesion!;
			obj.MaximoPublicaciones = (int?)dto.MaximoPublicaciones!;
			obj.IdGrupo = (int)dto.IdGrupo!;
			obj.Estatus = (string?)dto.Estatus!;
			obj.IdTipoUsuario = (int)dto.IdTipoUsuario!;
			obj.IdRol = (int?)dto.IdRol!;
			obj.UltimaIp = (string?)dto.UltimaIp!;
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
			obj.SegundoNombre = dto.SegundoNombre == null ? obj.SegundoNombre : (string?)dto.SegundoNombre;
			obj.SegundoApellido = dto.SegundoApellido == null ? obj.SegundoApellido : (string?)dto.SegundoApellido;
			obj.Perfil = dto.Perfil == null ? obj.Perfil : (string?)dto.Perfil;
			obj.Direccion = dto.Direccion == null ? obj.Direccion : dto.Direccion;
			obj.IdCiudad = dto.IdCiudad == null ? obj.IdCiudad : (int)dto.IdCiudad;
			obj.Telefono = dto.Telefono == null ? obj.Telefono : dto.Telefono;
			obj.Telefono2 = dto.Telefono2 == null ? obj.Telefono2 : (string?)dto.Telefono2;
			obj.Email = dto.Email == null ? obj.Email : dto.Email;
			obj.Clave = dto.Clave == null ? obj.Clave : Cripto.CodigoSHA256(dto.Clave);
			obj.Email2 = dto.Email2 == null ? obj.Email2 : (string?)dto.Email2;
			obj.IdProfesion = dto.IdProfesion == null ? obj.IdProfesion : (int?)dto.IdProfesion;
			obj.MaximoPublicaciones = dto.MaximoPublicaciones == null ? obj.MaximoPublicaciones : (int?)dto.MaximoPublicaciones;
			obj.IdGrupo = dto.IdGrupo == null ? obj.IdGrupo : (int)dto.IdGrupo;
			obj.Estatus = dto.Estatus == null ? obj.Estatus : (string?)dto.Estatus;
			obj.IdTipoUsuario = dto.IdTipoUsuario == null ? obj.IdTipoUsuario : (int)dto.IdTipoUsuario;
			obj.IdRol = dto.IdRol == null ? obj.IdRol : (int?)dto.IdRol;
			obj.UltimaIp = dto.UltimaIp == null ? obj.UltimaIp : (string?)dto.UltimaIp;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }



    private Guid AutenticarUsuario(ClaimsPrincipal claims)
    {
        try
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));
            return id;
        }
        catch (ArgumentNullException ex)
        {
            throw (new Excepcionador(ex)).ExcepcionAutenticacion();
        }
    }
}
