
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

public class ActividadProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ActividadProyectoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<ActividadProyecto> CrearActividadProyecto(ActividadProyectoDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorActividadProyecto vc = new ValidadorActividadProyecto(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                ActividadProyecto obj = new ActividadProyecto();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.ActividadesProyectos.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteActividadProyecto(List<ActividadProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorActividadProyecto vc = new ValidadorActividadProyecto(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    ActividadProyecto obj = new ActividadProyecto();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.ActividadesProyectos.Add(obj);
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

    public async Task<bool> ModificarActividadProyecto(ActividadProyectoDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorActividadProyecto vc = new ValidadorActividadProyecto(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.ActividadesProyectos.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteActividadProyecto(List<ActividadProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<ActividadProyecto> objs = new List<ActividadProyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorActividadProyecto vc = new ValidadorActividadProyecto(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    ActividadProyecto obj = new ActividadProyecto();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.ActividadesProyectos.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarActividadProyecto(long id, ClaimsPrincipal claims)
    {
        ActividadProyectoDTO pub = new ActividadProyectoDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarActividadProyecto(pub, claims);
    }

    public void Mapear(ActividadProyecto obj, ActividadProyectoDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdProyecto = (Guid)dto.IdProyecto!;
			obj.IdRutaProyecto = (int)dto.IdRutaProyecto!;
			obj.Secuencia = (int)dto.Secuencia!;
			obj.IdActividadRuta = (int)dto.IdActividadRuta!;
			obj.Descripcion = dto.Descripcion!;
			obj.FechaInicio = (DateTime)dto.FechaInicio!;
			obj.FechaFinalizacion = (DateTime?)dto.FechaFinalizacion!;
			obj.IdEjecutor = (Guid?)dto.IdEjecutor!;
			obj.IdRevisor = (Guid?)dto.IdRevisor!;
			obj.IdEstatusPublicacion = (int)dto.IdEstatusPublicacion!;
			obj.IdEstatusProyecto = (int)dto.IdEstatusProyecto!;
			obj.IdRevisadaPor = (Guid?)dto.IdRevisadaPor!;
			obj.IdTipoActividad = (int)dto.IdTipoActividad!;
			obj.TiempoEstimado = (int)dto.TiempoEstimado!;
			obj.ProgresoEstimado = (int)dto.ProgresoEstimado!;
			obj.Evaluacion = (decimal)dto.Evaluacion!;
			obj.FechaDisponible = (DateTime?)dto.FechaDisponible!;
			obj.TotalArticulos = (decimal)dto.TotalArticulos!;
			obj.CostoEstimado = (decimal)dto.CostoEstimado!;
			obj.IdMonedaCostoEstimado = dto.IdMonedaCostoEstimado!;
			obj.TipoCambioCostoEstimado = (decimal)dto.TipoCambioCostoEstimado!;
			obj.CostoReal = (decimal)dto.CostoReal!;
			obj.IdMonedaCostoReal = dto.IdMonedaCostoReal!;
			obj.TipoCambioCostoReal = (decimal)dto.TipoCambioCostoReal!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdProyecto = dto.IdProyecto == null ? obj.IdProyecto : (Guid)dto.IdProyecto;
			obj.IdRutaProyecto = dto.IdRutaProyecto == null ? obj.IdRutaProyecto : (int)dto.IdRutaProyecto;
			obj.Secuencia = dto.Secuencia == null ? obj.Secuencia : (int)dto.Secuencia;
			obj.IdActividadRuta = dto.IdActividadRuta == null ? obj.IdActividadRuta : (int)dto.IdActividadRuta;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.FechaInicio = dto.FechaInicio == null ? obj.FechaInicio : (DateTime)dto.FechaInicio;
			obj.FechaFinalizacion = dto.FechaFinalizacion == null ? obj.FechaFinalizacion : (DateTime?)dto.FechaFinalizacion;
			obj.IdEjecutor = dto.IdEjecutor == null ? obj.IdEjecutor : (Guid?)dto.IdEjecutor;
			obj.IdRevisor = dto.IdRevisor == null ? obj.IdRevisor : (Guid?)dto.IdRevisor;
			obj.IdEstatusPublicacion = dto.IdEstatusPublicacion == null ? obj.IdEstatusPublicacion : (int)dto.IdEstatusPublicacion;
			obj.IdEstatusProyecto = dto.IdEstatusProyecto == null ? obj.IdEstatusProyecto : (int)dto.IdEstatusProyecto;
			obj.IdRevisadaPor = dto.IdRevisadaPor == null ? obj.IdRevisadaPor : (Guid?)dto.IdRevisadaPor;
			obj.IdTipoActividad = dto.IdTipoActividad == null ? obj.IdTipoActividad : (int)dto.IdTipoActividad;
			obj.TiempoEstimado = dto.TiempoEstimado == null ? obj.TiempoEstimado : (int)dto.TiempoEstimado;
			obj.ProgresoEstimado = dto.ProgresoEstimado == null ? obj.ProgresoEstimado : (int)dto.ProgresoEstimado;
			obj.Evaluacion = dto.Evaluacion == null ? obj.Evaluacion : (decimal)dto.Evaluacion;
			obj.FechaDisponible = dto.FechaDisponible == null ? obj.FechaDisponible : (DateTime?)dto.FechaDisponible;
			obj.TotalArticulos = dto.TotalArticulos == null ? obj.TotalArticulos : (decimal)dto.TotalArticulos;
			obj.CostoEstimado = dto.CostoEstimado == null ? obj.CostoEstimado : (decimal)dto.CostoEstimado;
			obj.IdMonedaCostoEstimado = dto.IdMonedaCostoEstimado == null ? obj.IdMonedaCostoEstimado : dto.IdMonedaCostoEstimado;
			obj.TipoCambioCostoEstimado = dto.TipoCambioCostoEstimado == null ? obj.TipoCambioCostoEstimado : (decimal)dto.TipoCambioCostoEstimado;
			obj.CostoReal = dto.CostoReal == null ? obj.CostoReal : (decimal)dto.CostoReal;
			obj.IdMonedaCostoReal = dto.IdMonedaCostoReal == null ? obj.IdMonedaCostoReal : dto.IdMonedaCostoReal;
			obj.TipoCambioCostoReal = dto.TipoCambioCostoReal == null ? obj.TipoCambioCostoReal : (decimal)dto.TipoCambioCostoReal;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }

    public async Task<bool> EliminarLoteActividadProyecto(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<ActividadProyecto> objs = new List<ActividadProyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.ActividadesProyectos.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.ActividadesProyectos.UpdateRange(objs);

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
