
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

public class VariosService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public VariosService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<Varios> CrearVarios(VariosDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorVarios vc = new ValidadorVarios(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Varios obj = new Varios();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Varias.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteVarios(List<VariosDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorVarios vc = new ValidadorVarios(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Varios obj = new Varios();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Varias.Add(obj);
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

    public async Task<bool> ModificarVarios(VariosDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorVarios vc = new ValidadorVarios(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Varias.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteVarios(List<VariosDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Varios> objs = new List<Varios>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorVarios vc = new ValidadorVarios(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Varios obj = new Varios();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Varias.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarVarios(int id, ClaimsPrincipal claims)
    {
        VariosDTO pub = new VariosDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarVarios(pub, claims);
    }

    public void Mapear(Varios obj, VariosDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdTabla = dto.IdTabla!;
			obj.Descripcion = dto.Descripcion!;
			obj.Referencia = dto.Referencia!;
			obj.IdPadre = (int?)dto.IdPadre!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdTabla = dto.IdTabla == null ? obj.IdTabla : dto.IdTabla;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.Referencia = dto.Referencia == null ? obj.Referencia : dto.Referencia;
			obj.IdPadre = dto.IdPadre == null ? obj.IdPadre : (int?)dto.IdPadre;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }

    public async Task<bool> EliminarLoteVarios(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Varios> objs = new List<Varios>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Varias.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Varias.UpdateRange(objs);

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