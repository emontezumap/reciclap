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

public class RecursoPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public RecursoPublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<ICollection<Guid>> CrearRecursoPublicacion(List<RecursoPublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                ValidadorRecursoPublicacion vc = new ValidadorRecursoPublicacion(nuevo, Operacion.Creacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    RecursoPublicacion obj = new RecursoPublicacion();
                    Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                    ctx.RecursosPublicaciones.Add(obj);
                    codigos.Add(obj.Id);
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
            else
                throw (new Excepcionador(res)).ExcepcionDatosNoValidos();

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> ModificarRecursoPublicacion(List<RecursoPublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<RecursoPublicacion> objs = new List<RecursoPublicacion>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorRecursoPublicacion vc = new ValidadorRecursoPublicacion(modif, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var obj = await ctx.RecursosPublicaciones.FindAsync(modif.Id);

                    if (obj != null) {
                        Mapear(obj, modif, idUsr, Operacion.Modificacion);
                        objs.Add(obj);
                        codigos.Add(obj.Id);
                    }
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.RecursosPublicaciones.UpdateRange(objs);

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
            else
                throw (new Excepcionador(res)).ExcepcionDatosNoValidos();

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> EliminarRecursoPublicacion(List<Guid> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
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
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
            }

            return codigos;
        }
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
}
