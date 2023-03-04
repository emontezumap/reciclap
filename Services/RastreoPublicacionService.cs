
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

public class RastreoPublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public RastreoPublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<RastreoPublicacion> CrearRastreoPublicacion(RastreoPublicacionDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorRastreoPublicacion vc = new ValidadorRastreoPublicacion(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                RastreoPublicacion obj = new RastreoPublicacion();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.RastreoPublicaciones.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteRastreoPublicacion(List<RastreoPublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorRastreoPublicacion vc = new ValidadorRastreoPublicacion(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    RastreoPublicacion obj = new RastreoPublicacion();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.RastreoPublicaciones.Add(obj);
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

    public async Task<bool> ModificarRastreoPublicacion(RastreoPublicacionDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorRastreoPublicacion vc = new ValidadorRastreoPublicacion(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.RastreoPublicaciones.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteRastreoPublicacion(List<RastreoPublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<RastreoPublicacion> objs = new List<RastreoPublicacion>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorRastreoPublicacion vc = new ValidadorRastreoPublicacion(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    RastreoPublicacion obj = new RastreoPublicacion();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.RastreoPublicaciones.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarRastreoPublicacion(long id, ClaimsPrincipal claims)
    {
        RastreoPublicacionDTO pub = new RastreoPublicacionDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarRastreoPublicacion(pub, claims);
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

    public async Task<bool> EliminarLoteRastreoPublicacion(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<RastreoPublicacion> objs = new List<RastreoPublicacion>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.RastreoPublicaciones.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.RastreoPublicaciones.UpdateRange(objs);

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
