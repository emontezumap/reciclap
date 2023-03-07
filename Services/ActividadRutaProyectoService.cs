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

public class ActividadRutaProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ActividadRutaProyectoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<ICollection<int>> CrearActividadRutaProyecto(List<ActividadRutaProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                ValidadorActividadRutaProyecto vc = new ValidadorActividadRutaProyecto(nuevo, Operacion.Creacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    ActividadRutaProyecto obj = new ActividadRutaProyecto();
                    Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                    ctx.ActividadesRutasProyectos.Add(obj);
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

    public async Task<ICollection<int>> ModificarActividadRutaProyecto(List<ActividadRutaProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<ActividadRutaProyecto> objs = new List<ActividadRutaProyecto>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorActividadRutaProyecto vc = new ValidadorActividadRutaProyecto(modif, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var obj = await ctx.ActividadesRutasProyectos.FindAsync(modif.Id);

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
                ctx.ActividadesRutasProyectos.UpdateRange(objs);

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

    public async Task<ICollection<int>> EliminarActividadRutaProyecto(List<int> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<ActividadRutaProyecto> objs = new List<ActividadRutaProyecto>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.ActividadesRutasProyectos.FindAsync(id);

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
                ctx.ActividadesRutasProyectos.UpdateRange(objs);

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

    public void Mapear(ActividadRutaProyecto obj, ActividadRutaProyectoDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdRutaProyecto = (int)dto.IdRutaProyecto!;
			obj.Descripcion = dto.Descripcion!;
			obj.Secuencia = (int)dto.Secuencia!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdRutaProyecto = dto.IdRutaProyecto == null ? obj.IdRutaProyecto : (int)dto.IdRutaProyecto;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.Secuencia = dto.Secuencia == null ? obj.Secuencia : (int)dto.Secuencia;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }
}
