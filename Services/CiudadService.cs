using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Validadores;
using Herramientas;

namespace Services;

[ExtendObjectType("Mutacion")]
public class CiudadService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public CiudadService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Ciudad>> TodasLasCiudades()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.ToListAsync<Ciudad>();
        }
    }

    public async Task<Ciudad?> UnaCiudad(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.FindAsync(id);
        }
    }

    [Authorize]
    public async Task<Ciudad?> CrearCiudad(CiudadDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorCiudad vc = new ValidadorCiudad(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Ciudad ciudad = new Ciudad();
                ciudad = Mapear(ciudad, nuevo, Operacion.Creacion);
                ciudad.IdCreador = id;
                ciudad.IdModificador = id;

                try
                {
                    ctx.Ciudades.Add(ciudad);
                    await ctx.SaveChangesAsync();

                    return ciudad;
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

    [Authorize]
    public async Task<bool> ModificarCiudad(CiudadDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorCiudad vc = new ValidadorCiudad(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Ciudades.FindAsync(modif.Id);

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

    [Authorize(Policy = "Admin")]
    public async Task<bool> EliminarCiudad(Guid id, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            try
            {
                Ciudad o = new Ciudad() { Id = id };
                ctx.Ciudades.Remove(o);
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
    }

    // public async Task<bool> EliminarCiudades(ClaimsPrincipal claims, params Guid[] ids)
    // {
    //     using (var ctx = ctxFactory.CreateDbContext())
    //     {
    //         foreach (Guid id in ids)
    //         {
    //             Ciudad o = new Ciudad() { Id = id };
    //             ctx.Ciudades.Remove(o);
    //         }

    //         try
    //         {
    //             await ctx.SaveChangesAsync();
    //             return true;
    //         }
    //         catch (DbUpdateException ex)
    //         {
    //             // FALTA: Implementar tratamiento de error
    //             return false;
    //         }
    //         catch (Exception ex)
    //         {
    //             // FALTA: Implementar tratamiento de error
    //             return false;
    //         }
    //     }
    // }

    public Ciudad Mapear(Ciudad c, CiudadDTO dto, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
            c.Activo = dto.Activo;
            c.FechaCreacion = DateTime.UtcNow;
            c.FechaModificacion = DateTime.UtcNow;
            c.Id = Guid.NewGuid();
            c.IdEstado = (Guid)dto.IdEstado!;
            c.Nombre = dto.Nombre!;
        }
        else
        {
            c.Activo = dto.Activo == null ? c.Activo : dto.Activo;
            c.FechaModificacion = DateTime.UtcNow;
            c.IdEstado = dto.IdEstado == null ? c.IdEstado : (Guid)dto.IdEstado;
            c.Nombre = dto.Nombre == null ? c.Nombre : dto.Nombre;
        }

        return c;
    }
}