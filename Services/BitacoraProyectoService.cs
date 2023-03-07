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

    public async Task<ICollection<long>> CrearBitacoraProyecto(List<BitacoraProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(nuevo, Operacion.Creacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    BitacoraProyecto obj = new BitacoraProyecto();
                    Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                    ctx.BitacorasProyectos.Add(obj);
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

    public async Task<ICollection<long>> ModificarBitacoraProyecto(List<BitacoraProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorBitacoraProyecto vc = new ValidadorBitacoraProyecto(modif, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var obj = await ctx.BitacorasProyectos.FindAsync(modif.Id);

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
                ctx.BitacorasProyectos.UpdateRange(objs);

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

    public async Task<ICollection<long>> EliminarBitacoraProyecto(List<long> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<BitacoraProyecto> objs = new List<BitacoraProyecto>();
        ICollection<long> codigos = new List<long>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
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

            if (objs.Count > 0)
            {
                ctx.BitacorasProyectos.UpdateRange(objs);

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
}
