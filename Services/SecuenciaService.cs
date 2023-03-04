
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

public class SecuenciaService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public SecuenciaService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<Secuencia> CrearSecuencia(SecuenciaDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorSecuencia vc = new ValidadorSecuencia(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Secuencia obj = new Secuencia();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Secuencias.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteSecuencia(List<SecuenciaDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorSecuencia vc = new ValidadorSecuencia(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Secuencia obj = new Secuencia();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Secuencias.Add(obj);
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

    public async Task<bool> ModificarSecuencia(SecuenciaDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorSecuencia vc = new ValidadorSecuencia(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Secuencias.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteSecuencia(List<SecuenciaDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Secuencia> objs = new List<Secuencia>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorSecuencia vc = new ValidadorSecuencia(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Secuencia obj = new Secuencia();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Secuencias.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarSecuencia(int id, ClaimsPrincipal claims)
    {
        SecuenciaDTO pub = new SecuenciaDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarSecuencia(pub, claims);
    }

    public void Mapear(Secuencia obj, SecuenciaDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Prefijo = dto.Prefijo!;
			obj.Serie = (long)dto.Serie!;
			obj.Incremento = (int)dto.Incremento!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.Prefijo = dto.Prefijo == null ? obj.Prefijo : dto.Prefijo;
			obj.Serie = dto.Serie == null ? obj.Serie : (long)dto.Serie;
			obj.Incremento = dto.Incremento == null ? obj.Incremento : (int)dto.Incremento;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }

    public async Task<bool> EliminarLoteSecuencia(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Secuencia> objs = new List<Secuencia>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Secuencias.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Secuencias.UpdateRange(objs);

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
