using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class RolService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public RolService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Rol>> TodosLosRoles()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Roles.ToListAsync<Rol>();
        }
    }

    public async Task<Rol?> UnRol(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Roles.FindAsync(id);
        }
    }

    public async Task<Rol> CrearRol(RolDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorRol vc = new ValidadorRol(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Rol rol = new Rol()
                {
                    Activo = nuevo.Activo,
                    Descripcion = nuevo.Descripcion!,
                    EsCreador = nuevo.EsCreador == null ? false : (bool)nuevo.EsCreador,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdModificador = id
                };

                try
                {
                    ctx.Roles.Add(rol);
                    await ctx.SaveChangesAsync();
                    return rol;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "El rol");
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException);
                }
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarRol(RolDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorRol vc = new ValidadorRol(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Roles.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                    buscado.EsCreador = modif.EsCreador == null ? buscado.EsCreador : (bool)modif.EsCreador;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdModificador = id;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "El rol");
                    }
                    catch (Exception ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException);
                    }
                }
                else
                    throw (new Excepcionador()).ExcepcionRegistroEliminado();
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> EliminarRol(Guid id, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            try
            {
                Rol o = new Rol() { Id = id };
                ctx.Roles.Remove(o);
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "El rol");
            }
            catch (Exception ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException);
            }
        }
        // RolDTO rol = new RolDTO()
        // {
        //     Id = id,
        //     Activo = false
        // };

        // return await ModificarRol(rol, claims);
    }
}