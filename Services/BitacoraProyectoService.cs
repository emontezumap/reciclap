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

public class BitacoraProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorBitacoraProyectoRequeridos validadorReqs;
    private readonly ValidadorBitacoraProyecto validador;

    public BitacoraProyectoService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorBitacoraProyectoRequeridos validadorReqs, ValidadorBitacoraProyecto validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<long>> CrearBitacoraProyecto(List<BitacoraProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                BitacoraProyecto obj = new BitacoraProyecto();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);

                var v = ctx.BitacorasProyectos.Add(obj);
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

    public async Task<ICollection<long>> ModificarBitacoraProyecto(List<BitacoraProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.BitacorasProyectos.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Creacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.BitacorasProyectos.UpdateRange(objs);

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

    public async Task<ICollection<long>> EliminarBitacoraProyecto(List<long> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var id in ids)
            {
                var buscado = await ctx.BitacorasProyectos.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            ctx.BitacorasProyectos.UpdateRange(objs);

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

    private void ValidarDatos(BitacoraProyectoDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(BitacoraProyecto obj, BitacoraProyectoDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdProyecto = (Guid)dto.IdProyecto!;
			obj.IdActividadProyecto = (long)dto.IdActividadProyecto!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.IdUsuario = (Guid)dto.IdUsuario!;
			obj.IdTipoBitacora = (int)dto.IdTipoBitacora!;
			obj.Comentarios = dto.Comentarios!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdProyecto = dto.IdProyecto == null ? obj.IdProyecto : (Guid)dto.IdProyecto;
			obj.IdActividadProyecto = dto.IdActividadProyecto == null ? obj.IdActividadProyecto : (long)dto.IdActividadProyecto;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.IdUsuario = dto.IdUsuario == null ? obj.IdUsuario : (Guid)dto.IdUsuario;
			obj.IdTipoBitacora = dto.IdTipoBitacora == null ? obj.IdTipoBitacora : (int)dto.IdTipoBitacora;
			obj.Comentarios = dto.Comentarios == null ? obj.Comentarios : dto.Comentarios;
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
