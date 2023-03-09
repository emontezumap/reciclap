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

public class RastreoPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorRastreoPublicacionRequeridos validadorReqs;
    private readonly ValidadorRastreoPublicacion validador;

    public RastreoPublicacionService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorRastreoPublicacionRequeridos validadorReqs, ValidadorRastreoPublicacion validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<long>> CrearRastreoPublicacion(List<RastreoPublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                RastreoPublicacion obj = new RastreoPublicacion();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.RastreoPublicaciones.Add(obj);
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

    public async Task<ICollection<long>> ModificarRastreoPublicacion(List<RastreoPublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<RastreoPublicacion> objs = new List<RastreoPublicacion>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.RastreoPublicaciones.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Modificacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.RastreoPublicaciones.UpdateRange(objs);

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

    public async Task<ICollection<long>> EliminarRastreoPublicacion(List<long> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<RastreoPublicacion> objs = new List<RastreoPublicacion>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.RastreoPublicaciones.FindAsync(id);

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
                ctx.RastreoPublicaciones.UpdateRange(objs);

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

    private void ValidarDatos(RastreoPublicacionDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(RastreoPublicacion obj, RastreoPublicacionDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdPublicacion = (Guid)dto.IdPublicacion!;
			obj.IdFasePublicacion = (int)dto.IdFasePublicacion!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.IdUsuario = (Guid)dto.IdUsuario!;
			obj.TiempoEstimado = (long)dto.TiempoEstimado!;
			obj.IdFaseAnterior = (int?)dto.IdFaseAnterior!;
			obj.IdFaseSiguiente = (int?)dto.IdFaseSiguiente!;
			obj.Comentarios = dto.Comentarios!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdPublicacion = dto.IdPublicacion == null ? obj.IdPublicacion : (Guid)dto.IdPublicacion;
			obj.IdFasePublicacion = dto.IdFasePublicacion == null ? obj.IdFasePublicacion : (int)dto.IdFasePublicacion;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.IdUsuario = dto.IdUsuario == null ? obj.IdUsuario : (Guid)dto.IdUsuario;
			obj.TiempoEstimado = dto.TiempoEstimado == null ? obj.TiempoEstimado : (long)dto.TiempoEstimado;
			obj.IdFaseAnterior = dto.IdFaseAnterior == null ? obj.IdFaseAnterior : (int?)dto.IdFaseAnterior;
			obj.IdFaseSiguiente = dto.IdFaseSiguiente == null ? obj.IdFaseSiguiente : (int?)dto.IdFaseSiguiente;
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
