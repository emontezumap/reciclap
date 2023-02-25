using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
// using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Validadores;
using Herramientas;
using DB;
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

    public async Task<Varios?> CrearVarios(VariosDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorVarios vc = new ValidadorVarios(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                Varios varios = new Varios();
                varios = Mapear(varios, nuevo, Operacion.Creacion);
                varios.IdCreador = id;
                varios.IdModificador = id;

                try
                {
                    ctx.Varias.Add(varios);
                    await ctx.SaveChangesAsync();

                    return varios;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La ciudad");
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

    public async Task<bool> ModificarCiudad(VariosDTO modif, ClaimsPrincipal claims)
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
                    buscado = Mapear(buscado, modif, Operacion.Modificacion);
                    buscado.IdModificador = id;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La ciudad");
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

    public async Task<int> ModificarCiudades(ICollection<VariosDTO> listaVarios, ClaimsPrincipal claims)
    {
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<ResultadoValidacion> rvs = new List<ResultadoValidacion>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (VariosDTO v in listaVarios)
            {
                ValidadorVarios vc = new ValidadorVarios(v, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var buscado = await ctx.Varias.FindAsync(v.Id);

                    if (buscado != null)
                    {
                        buscado = Mapear(buscado, v, Operacion.Modificacion);
                        buscado.IdModificador = id;
                    }
                    else
                        throw (new Excepcionador()).ExcepcionRegistroEliminado();
                }
                else
                {
                    rvs.Append(rv);
                }
            }

            if (rvs.Count == 0)
            {
                try
                {
                    return await ctx.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La ciudad");
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                }
            }
            else
            {
                // FALTA: Lanzar excepcion con los resultados de las validacines
                throw (new Excepcionador(rvs)).ExcepcionDatosNoValidos();
            }
        }
    }

    public async Task<bool> EliminarVarios(int id, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            try
            {
                var buscado = await ctx.Varias.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    await ctx.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw (new Excepcionador()).ExcepcionRegistroNoExiste();
                }
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La ciudad");
            }
            catch (Exception ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
            }
        }
    }

    public Varios Mapear(Varios v, VariosDTO dto, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
            v.Activo = dto.Activo;
            v.FechaCreacion = DateTime.UtcNow;
            v.FechaModificacion = DateTime.UtcNow;
            v.IdPadre = dto.IdPadre!;
            v.IdTabla = dto.IdTabla!;
            v.Referencia = dto.Referencia == null ? "" : dto.Referencia;
        }
        else
        {
            v.Activo = dto.Activo == null ? v.Activo : dto.Activo;
            v.FechaModificacion = DateTime.UtcNow;
            v.IdPadre = dto.IdPadre == null ? v.IdPadre : dto.IdPadre;
            v.IdTabla = dto.IdTabla == null ? v.IdTabla : dto.IdTabla;
            v.Referencia = dto.Referencia == null ? v.Referencia : dto.Referencia;
        }

        return v;
    }
}
