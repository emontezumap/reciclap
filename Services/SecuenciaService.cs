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
        using (var ctx = ctxFactory.CreateDbContext())
        {
            this.ctxFactory = ctxFactory;
        }
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
                Secuencia sec = new Secuencia()
                {
                    Activo = nuevo.Activo,
                    Prefijo = nuevo.Prefijo!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Serie = nuevo.Serie == null ? 1 : (int)nuevo.Serie,
                    IdCreador = id,
                    Incremento = nuevo.Incremento == null ? 1 : (int)nuevo.Incremento,
                };

                try
                {
                    ctx.Secuencias.Add(sec);
                    await ctx.SaveChangesAsync();
                    return sec;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La publicación");
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

    public async Task<bool> ModificarSecuencia(SecuenciaDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorSecuencia vs = new ValidadorSecuencia(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vs.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Secuencias.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Prefijo = modif.Prefijo == null ? buscado.Prefijo : modif.Prefijo;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Serie = modif.Serie == null ? buscado.Serie : (long)modif.Serie;
                    buscado.IdModificador = id;
                    buscado.Incremento = modif.Incremento == null ? buscado.Incremento : (int)modif.Incremento;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La secuencia");
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

    public async Task<bool> EliminarSecuencia(int id, ClaimsPrincipal claims)
    {
        SecuenciaDTO dto = new SecuenciaDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarSecuencia(dto, claims);

    }
}