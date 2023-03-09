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

public class PersonalService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorPersonalRequeridos validadorReqs;
    private readonly ValidadorPersonal validador;

    public PersonalService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorPersonalRequeridos validadorReqs, ValidadorPersonal validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<long>> CrearPersonal(List<PersonalDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                Personal obj = new Personal();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.Personal.Add(obj);
                codigos.Add(v.Entity.Id);
            }

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<long>> ModificarPersonal(List<PersonalDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Personal> objs = new List<Personal>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Personal.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Modificacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Personal.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<long>> EliminarPersonal(List<long> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Personal> objs = new List<Personal>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Personal.FindAsync(id);

                if (buscado != null)
                {
                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;
                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Personal.UpdateRange(objs);

                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
                }
            }

            return codigos;
        }
    }

    private void ValidarDatos(PersonalDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(Personal obj, PersonalDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdPublicacion = (Guid)dto.IdPublicacion!;
			obj.IdUsuario = (Guid)dto.IdUsuario!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.IdRol = (int)dto.IdRol!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdPublicacion = dto.IdPublicacion == null ? obj.IdPublicacion : (Guid)dto.IdPublicacion;
			obj.IdUsuario = dto.IdUsuario == null ? obj.IdUsuario : (Guid)dto.IdUsuario;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.IdRol = dto.IdRol == null ? obj.IdRol : (int)dto.IdRol;
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
