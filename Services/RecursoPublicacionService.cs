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

public class RecursoPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorRecursoPublicacionRequeridos validadorReqs;
    private readonly ValidadorRecursoPublicacion validador;

    public RecursoPublicacionService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorRecursoPublicacionRequeridos validadorReqs, ValidadorRecursoPublicacion validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<Guid>> CrearRecursoPublicacion(List<RecursoPublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                RecursoPublicacion obj = new RecursoPublicacion();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.RecursosPublicaciones.Add(obj);
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

    public async Task<ICollection<Guid>> ModificarRecursoPublicacion(List<RecursoPublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<RecursoPublicacion> objs = new List<RecursoPublicacion>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.RecursosPublicaciones.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Modificacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.RecursosPublicaciones.UpdateRange(objs);

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

    public async Task<ICollection<Guid>> EliminarRecursoPublicacion(List<Guid> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<RecursoPublicacion> objs = new List<RecursoPublicacion>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.RecursosPublicaciones.FindAsync(id);

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
                ctx.RecursosPublicaciones.UpdateRange(objs);

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

    private void ValidarDatos(RecursoPublicacionDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(RecursoPublicacion obj, RecursoPublicacionDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.IdTipoCatalogo = (int)dto.IdTipoCatalogo!;
			obj.IdCatalogo = (Guid)dto.IdCatalogo!;
			obj.Secuencia = (int)dto.Secuencia!;
			obj.IdTipoRecurso = (int)dto.IdTipoRecurso!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.IdUsuario = (Guid)dto.IdUsuario!;
			obj.Orden = (int)dto.Orden!;
			obj.Nombre = dto.Nombre!;
			obj.IdEstatusRecurso = (int)dto.IdEstatusRecurso!;
			obj.FechaExpiracion = (DateTime?)dto.FechaExpiracion!;
			obj.Tamano = (long)dto.Tamano!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdTipoCatalogo = dto.IdTipoCatalogo == null ? obj.IdTipoCatalogo : (int)dto.IdTipoCatalogo;
			obj.IdCatalogo = dto.IdCatalogo == null ? obj.IdCatalogo : (Guid)dto.IdCatalogo;
			obj.Secuencia = dto.Secuencia == null ? obj.Secuencia : (int)dto.Secuencia;
			obj.IdTipoRecurso = dto.IdTipoRecurso == null ? obj.IdTipoRecurso : (int)dto.IdTipoRecurso;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.IdUsuario = dto.IdUsuario == null ? obj.IdUsuario : (Guid)dto.IdUsuario;
			obj.Orden = dto.Orden == null ? obj.Orden : (int)dto.Orden;
			obj.Nombre = dto.Nombre == null ? obj.Nombre : dto.Nombre;
			obj.IdEstatusRecurso = dto.IdEstatusRecurso == null ? obj.IdEstatusRecurso : (int)dto.IdEstatusRecurso;
			obj.FechaExpiracion = dto.FechaExpiracion == null ? obj.FechaExpiracion : (DateTime?)dto.FechaExpiracion;
			obj.Tamano = dto.Tamano == null ? obj.Tamano : (long)dto.Tamano;
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
