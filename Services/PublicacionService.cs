using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class PublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public PublicacionService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            this.ctxFactory = ctxFactory;
        }
    }

    public async Task<IEnumerable<Publicacion>> TodasLasPublicaciones()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Publicaciones.ToListAsync<Publicacion>();
        }
    }

    public async Task<Publicacion?> UnaPublicacion(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Publicaciones.FindAsync(id);
        }
    }

    public async Task<Publicacion> CrearPublicacion(PublicacionDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPublicacion vc = new ValidadorPublicacion(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Publicacion pub = new Publicacion()
                {
                    Activo = nuevo.Activo,
                    Descripcion = nuevo.Descripcion!,
                    Fecha = (DateTime)nuevo.Fecha!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Gustan = nuevo.Gustan == null ? 0 : (int)nuevo.Gustan,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdEstatus = (Guid)nuevo.IdEstatus!,
                    IdModificador = id,
                    IdTipoPublicacion = (Guid)nuevo.IdTipoPublicacion!,
                    NoGustan = nuevo.NoGustan == null ? 0 : (int)nuevo.NoGustan,
                    Titulo = nuevo.Titulo!,
                };

                try
                {
                    ctx.Publicaciones.Add(pub);
                    await ctx.SaveChangesAsync();
                    return pub;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "La publicación");
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

    public async Task<bool> ModificarPublicacion(PublicacionDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorPublicacion vc = new ValidadorPublicacion(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Publicaciones.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                    buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Gustan = modif.Gustan == null ? buscado.Gustan : (int)modif.Gustan;
                    buscado.IdEstatus = modif.IdEstatus == null ? buscado.IdEstatus : (Guid)modif.IdEstatus;
                    buscado.IdModificador = id;
                    buscado.IdTipoPublicacion = modif.IdTipoPublicacion == null ? buscado.IdTipoPublicacion : (Guid)modif.IdTipoPublicacion;
                    buscado.NoGustan = modif.NoGustan == null ? buscado.NoGustan : (int)modif.NoGustan;
                    buscado.Titulo = modif.Titulo == null ? buscado.Titulo : modif.Titulo;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "La publicación");
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

    public async Task<bool> EliminarPublicacion(Guid id, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            try
            {
                Publicacion o = new Publicacion() { Id = id };
                ctx.Publicaciones.Remove(o);
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException, "La publicación");
            }
            catch (Exception ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex.InnerException);
            }
        }
        // PublicacionDTO pub = new PublicacionDTO()
        // {
        //     Id = id,
        //     Activo = false
        // };

        // return await ModificarPublicacion(pub, claims);
    }
}