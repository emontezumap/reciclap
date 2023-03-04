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

                using (var tranCtx = ctx.Database.BeginTransaction())
                {
                    Secuencia sec = (Secuencia)ctx.Secuencias
                        .Where(s => s.Prefijo == SECUENCIAS.PUB);

                    Publicacion pub = new Publicacion()
                    {
                        Activo = nuevo.Activo,
                        Consecutivo = sec == null ? 1 : sec.Serie + 1,
                        CostoEstimado = (decimal)nuevo.CostoEstimado!,
                        CostoReal = (decimal)nuevo.CostoReal!,
                        CostoRealTraslado = (decimal)nuevo.CostoRealTraslado!,
                        Descripcion = nuevo.Descripcion!,
                        Direccion = nuevo.Direccion!,
                        DireccionIPCreacion = nuevo.DireccionIPCreacion!,
                        Dispositivo = nuevo.Dispositivo!,
                        Evaluacion = (decimal)nuevo.Evaluacion!,
                        Fecha = (DateTime)nuevo.Fecha!,
                        FechaCreacion = DateTime.UtcNow,
                        FechaDisponible = (DateTime)nuevo.FechaDisponible!,
                        FechaModificacion = DateTime.UtcNow,
                        Gustan = nuevo.Gustan == null ? 0 : (int)nuevo.Gustan,
                        Id = Guid.NewGuid(),
                        IdCreador = id,
                        IdClasePublicacion = (int)nuevo.IdClasePublicacion!,
                        IdEstatusPublicacion = (int)nuevo.IdEstatusPublicacion!,
                        IdFasePublicacion = (int)nuevo.IdFasePublicacion!,
                        IdImagenPrincipal = (Guid)nuevo.IdImagenPrincipal!,
                        IdModificador = id,
                        IdMonedaCostoEstimado = nuevo.IdMonedaCostoEstimado!,
                        IdMonedaCostoReal = nuevo.IdMonedaCostoReal!,
                        IdProyecto = (Guid)nuevo.IdProyecto!,
                        IdPublicador = (Guid)nuevo.IdPublicador!,
                        IdRevisadaPor = (Guid)nuevo.IdRevisadaPor!,
                        IdTipoPublicacion = (int)nuevo.IdTipoPublicacion!,
                        MontoInversion = (decimal)nuevo.MontoInversion!,
                        NoGustan = nuevo.NoGustan == null ? 0 : (int)nuevo.NoGustan,
                        Posicionamiento = (int)nuevo.Posicionamiento!,
                        ReferenciasDireccion = nuevo.ReferenciasDireccion!,
                        Secuencia = (int)nuevo.Secuencia!,
                        TiempoEstimado = (int)nuevo.TiempoEstimado!,
                        TipoCambioCostoEstimado = (decimal)nuevo.TipoCambioCostoEstimado!,
                        TipoCambioCostoReal = (decimal)nuevo.TipoCambioCostoReal!,
                        Titulo = nuevo.Titulo!,
                        TotalArticulos = (decimal)nuevo.TotalArticulos!,
                        Vistas = (int)nuevo.Vistas!
                    };

                    if (sec != null)
                        sec.Serie++;

                    try
                    {
                        ctx.Publicaciones.Add(pub);
                        await ctx.SaveChangesAsync();
                        tranCtx.Commit();

                        return pub;
                    }
                    catch (DbUpdateException ex)
                    {
                        tranCtx.Rollback();
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "La publicación");
                    }
                    catch (Exception ex)
                    {
                        tranCtx.Rollback();
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
                    }
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

                    buscado.CostoEstimado = modif.CostoEstimado == null ? buscado.CostoEstimado : (decimal)modif.CostoEstimado;
                    buscado.CostoReal = modif.CostoReal == null ? buscado.CostoReal : (decimal)modif.CostoReal;
                    buscado.CostoRealTraslado = modif.CostoRealTraslado == null ? buscado.CostoRealTraslado : (decimal)modif.CostoRealTraslado;
                    buscado.Descripcion = modif.Descripcion == null ? buscado.Descripcion : modif.Descripcion;
                    buscado.Direccion = modif.Direccion == null ? buscado.Direccion : modif.Direccion;
                    buscado.DireccionIPCreacion = modif.DireccionIPCreacion == null ? buscado.DireccionIPCreacion : modif.DireccionIPCreacion;
                    buscado.Dispositivo = modif.Dispositivo == null ? buscado.Dispositivo : modif.Dispositivo;
                    buscado.Evaluacion = modif.Evaluacion == null ? buscado.Evaluacion : (decimal)modif.Evaluacion;
                    buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                    buscado.FechaDisponible = modif.FechaDisponible == null ? buscado.FechaDisponible : (DateTime)modif.FechaDisponible;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Gustan = modif.Gustan == null ? buscado.Gustan : (int)modif.Gustan;
                    buscado.IdClasePublicacion = modif.IdClasePublicacion == null ? buscado.IdClasePublicacion : (int)modif.IdClasePublicacion;
                    buscado.IdEstatusPublicacion = modif.IdEstatusPublicacion == null ? buscado.IdEstatusPublicacion : (int)modif.IdEstatusPublicacion;
                    buscado.IdFasePublicacion = modif.IdFasePublicacion == null ? buscado.IdFasePublicacion : (int)modif.IdFasePublicacion;
                    buscado.IdImagenPrincipal = modif.IdImagenPrincipal == null ? buscado.IdImagenPrincipal : (Guid)modif.IdImagenPrincipal;
                    buscado.IdModificador = id;
                    buscado.IdMonedaCostoEstimado = modif.IdMonedaCostoEstimado == null ? buscado.IdMonedaCostoEstimado : modif.IdMonedaCostoEstimado;
                    buscado.IdMonedaCostoReal = modif.IdMonedaCostoReal == null ? buscado.IdMonedaCostoReal : modif.IdMonedaCostoReal;
                    buscado.IdProyecto = modif.IdProyecto == null ? buscado.IdProyecto : (Guid)modif.IdProyecto;
                    buscado.IdPublicador = modif.IdPublicador == null ? buscado.IdPublicador : (Guid)modif.IdPublicador;
                    buscado.IdRevisadaPor = modif.IdRevisadaPor == null ? buscado.IdRevisadaPor : (Guid)modif.IdRevisadaPor;
                    buscado.IdTipoPublicacion = modif.IdTipoPublicacion == null ? buscado.IdTipoPublicacion : (int)modif.IdTipoPublicacion;
                    buscado.MontoInversion = modif.MontoInversion == null ? buscado.MontoInversion : (decimal)modif.MontoInversion;
                    buscado.NoGustan = modif.NoGustan == null ? buscado.NoGustan : (int)modif.NoGustan;
                    buscado.Posicionamiento = modif.Posicionamiento == null ? buscado.Posicionamiento : (int)modif.Posicionamiento;
                    buscado.ReferenciasDireccion = modif.ReferenciasDireccion == null ? buscado.ReferenciasDireccion : modif.ReferenciasDireccion;
                    buscado.Secuencia = modif.Secuencia == null ? buscado.Secuencia : (int)modif.Secuencia;
                    buscado.TiempoEstimado = modif.TiempoEstimado == null ? buscado.TiempoEstimado : (int)modif.TiempoEstimado;
                    buscado.TipoCambioCostoEstimado = modif.TipoCambioCostoEstimado == null ? buscado.TipoCambioCostoEstimado : (decimal)modif.TipoCambioCostoEstimado;
                    buscado.TipoCambioCostoReal = modif.TipoCambioCostoReal == null ? buscado.TipoCambioCostoReal : (decimal)modif.TipoCambioCostoReal;
                    buscado.Titulo = modif.Titulo == null ? buscado.Titulo : modif.Titulo;
                    buscado.TotalArticulos = modif.TotalArticulos == null ? buscado.TotalArticulos : (decimal)modif.TotalArticulos;
                    buscado.Vistas = modif.Vistas == null ? buscado.Vistas : (int)modif.Vistas;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
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
                    throw (new Excepcionador()).ExcepcionRegistroEliminado();
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> EliminarPublicacion(Guid id, ClaimsPrincipal claims)
    {
        PublicacionDTO pub = new PublicacionDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarPublicacion(pub, claims);
    }
}