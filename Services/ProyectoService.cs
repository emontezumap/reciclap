
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

public class ProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ProyectoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<Proyecto> CrearProyecto(ProyectoDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorProyecto vc = new ValidadorProyecto(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Proyecto obj = new Proyecto();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Proyectos.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteProyecto(List<ProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorProyecto vc = new ValidadorProyecto(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Proyecto obj = new Proyecto();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Proyectos.Add(obj);
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

    public async Task<bool> ModificarProyecto(ProyectoDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorProyecto vc = new ValidadorProyecto(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Proyectos.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteProyecto(List<ProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Proyecto> objs = new List<Proyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorProyecto vc = new ValidadorProyecto(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Proyecto obj = new Proyecto();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Proyectos.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarProyecto(Guid id, ClaimsPrincipal claims)
    {
        ProyectoDTO pub = new ProyectoDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarProyecto(pub, claims);
    }

    public void Mapear(Proyecto obj, ProyectoDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.Titulo = dto.Titulo!;
			obj.Descripcion = dto.Descripcion!;
			obj.FechaInicio = (DateTime)dto.FechaInicio!;
			obj.IdGerente = (Guid)dto.IdGerente!;
			obj.IdRevisor = (Guid?)dto.IdRevisor!;
			obj.Gustan = (int)dto.Gustan!;
			obj.NoGustan = (int)dto.NoGustan!;
			obj.IdEstatusPublicacion = (int)dto.IdEstatusPublicacion!;
			obj.IdEstatusProyecto = (int)dto.IdEstatusProyecto!;
			obj.IdRevisadaPor = (Guid)dto.IdRevisadaPor!;
			obj.IdImagenPrincipal = (Guid?)dto.IdImagenPrincipal!;
			obj.IdTipoProyecto = (int)dto.IdTipoProyecto!;
			obj.TiempoEstimado = (int)dto.TiempoEstimado!;
			obj.ProgresoEstimado = (int)dto.ProgresoEstimado!;
			obj.ProgresoReal = (int)dto.ProgresoReal!;
			obj.Evaluacion = (decimal)dto.Evaluacion!;
			obj.IdRutaProyecto = (int)dto.IdRutaProyecto!;
			obj.IdFaseAnterior = (int?)dto.IdFaseAnterior!;
			obj.IdFaseSiguiente = (int?)dto.IdFaseSiguiente!;
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
			obj.Titulo = dto.Titulo == null ? obj.Titulo : dto.Titulo;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.FechaInicio = dto.FechaInicio == null ? obj.FechaInicio : (DateTime)dto.FechaInicio;
			obj.IdGerente = dto.IdGerente == null ? obj.IdGerente : (Guid)dto.IdGerente;
			obj.IdRevisor = dto.IdRevisor == null ? obj.IdRevisor : (Guid?)dto.IdRevisor;
			obj.Gustan = dto.Gustan == null ? obj.Gustan : (int)dto.Gustan;
			obj.NoGustan = dto.NoGustan == null ? obj.NoGustan : (int)dto.NoGustan;
			obj.IdEstatusPublicacion = dto.IdEstatusPublicacion == null ? obj.IdEstatusPublicacion : (int)dto.IdEstatusPublicacion;
			obj.IdEstatusProyecto = dto.IdEstatusProyecto == null ? obj.IdEstatusProyecto : (int)dto.IdEstatusProyecto;
			obj.IdRevisadaPor = dto.IdRevisadaPor == null ? obj.IdRevisadaPor : (Guid)dto.IdRevisadaPor;
			obj.IdImagenPrincipal = dto.IdImagenPrincipal == null ? obj.IdImagenPrincipal : (Guid?)dto.IdImagenPrincipal;
			obj.IdTipoProyecto = dto.IdTipoProyecto == null ? obj.IdTipoProyecto : (int)dto.IdTipoProyecto;
			obj.TiempoEstimado = dto.TiempoEstimado == null ? obj.TiempoEstimado : (int)dto.TiempoEstimado;
			obj.ProgresoEstimado = dto.ProgresoEstimado == null ? obj.ProgresoEstimado : (int)dto.ProgresoEstimado;
			obj.ProgresoReal = dto.ProgresoReal == null ? obj.ProgresoReal : (int)dto.ProgresoReal;
			obj.Evaluacion = dto.Evaluacion == null ? obj.Evaluacion : (decimal)dto.Evaluacion;
			obj.IdRutaProyecto = dto.IdRutaProyecto == null ? obj.IdRutaProyecto : (int)dto.IdRutaProyecto;
			obj.IdFaseAnterior = dto.IdFaseAnterior == null ? obj.IdFaseAnterior : (int?)dto.IdFaseAnterior;
			obj.IdFaseSiguiente = dto.IdFaseSiguiente == null ? obj.IdFaseSiguiente : (int?)dto.IdFaseSiguiente;
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

    public async Task<bool> EliminarLoteProyecto(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Proyecto> objs = new List<Proyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Proyectos.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Proyectos.UpdateRange(objs);

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
