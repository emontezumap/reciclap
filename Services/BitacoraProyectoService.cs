
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

public class BitacoraProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public BitacoraProyectoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<BitacoraProyecto> CrearBitacoraProyecto(BitacoraProyectoDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                BitacoraProyecto obj = new BitacoraProyecto();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.BitacorasProyectos.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteBitacoraProyecto(List<BitacoraProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    BitacoraProyecto obj = new BitacoraProyecto();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.BitacorasProyectos.Add(obj);
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

    public async Task<bool> ModificarBitacoraProyecto(BitacoraProyectoDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.BitacorasProyectos.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteBitacoraProyecto(List<BitacoraProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    BitacoraProyecto obj = new BitacoraProyecto();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.BitacorasProyectos.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarBitacoraProyecto(long id, ClaimsPrincipal claims)
    {
        BitacoraProyectoDTO pub = new BitacoraProyectoDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarBitacoraProyecto(pub, claims);
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

    public async Task<bool> EliminarLoteBitacoraProyecto(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.BitacorasProyectos.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.BitacorasProyectos.UpdateRange(objs);

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
