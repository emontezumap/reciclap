
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
        this.ctxFactory = ctxFactory;
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
                Publicacion obj = new Publicacion();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Publicaciones.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLotePublicacion(List<PublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorPublicacion vc = new ValidadorPublicacion(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Publicacion obj = new Publicacion();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Publicaciones.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLotePublicacion(List<PublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Publicacion> objs = new List<Publicacion>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorPublicacion vc = new ValidadorPublicacion(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Publicacion obj = new Publicacion();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Publicaciones.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
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

    public void Mapear(Publicacion obj, PublicacionDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.Titulo = dto.Titulo!;
			obj.Descripcion = dto.Descripcion!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.Consecutivo = (int)dto.Consecutivo!;
			obj.IdPublicador = (Guid)dto.IdPublicador!;
			obj.Gustan = (int)dto.Gustan!;
			obj.NoGustan = (int)dto.NoGustan!;
			obj.IdEstatusPublicacion = (int)dto.IdEstatusPublicacion!;
			obj.IdFasePublicacion = (int)dto.IdFasePublicacion!;
			obj.IdTipoPublicacion = (int)dto.IdTipoPublicacion!;
			obj.IdClasePublicacion = (int)dto.IdClasePublicacion!;
			obj.IdRevisadaPor = (Guid?)dto.IdRevisadaPor!;
			obj.IdImagenPrincipal = (Guid?)dto.IdImagenPrincipal!;
			obj.TiempoEstimado = (int)dto.TiempoEstimado!;
			obj.Posicionamiento = (int)dto.Posicionamiento!;
			obj.Secuencia = (int)dto.Secuencia!;
			obj.Vistas = (int)dto.Vistas!;
			obj.Evaluacion = (decimal)dto.Evaluacion!;
			obj.DireccionIpCreacion = dto.DireccionIpCreacion!;
			obj.Dispositivo = dto.Dispositivo!;
			obj.Direccion = dto.Direccion!;
			obj.ReferenciasDireccion = dto.ReferenciasDireccion!;
			obj.FechaDisponible = (DateTime?)dto.FechaDisponible!;
			obj.TotalArticulos = (int)dto.TotalArticulos!;
			obj.IdProyecto = (Guid?)dto.IdProyecto!;
			obj.CostoEstimado = (decimal)dto.CostoEstimado!;
			obj.IdMonedaCostoEstimado = dto.IdMonedaCostoEstimado!;
			obj.TipoCambioCostoEstimado = (decimal)dto.TipoCambioCostoEstimado!;
			obj.CostoReal = (decimal)dto.CostoReal!;
			obj.MontoInversion = (decimal)dto.MontoInversion!;
			obj.CostoRealTraslado = (decimal)dto.CostoRealTraslado!;
			obj.IdMonedaCostoReal = dto.IdMonedaCostoReal!;
			obj.TipoCambioCostoReal = (decimal)dto.TipoCambioCostoReal!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.Titulo = dto.Titulo == null ? obj.Titulo : dto.Titulo;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.Consecutivo = dto.Consecutivo == null ? obj.Consecutivo : (int)dto.Consecutivo;
			obj.IdPublicador = dto.IdPublicador == null ? obj.IdPublicador : (Guid)dto.IdPublicador;
			obj.Gustan = dto.Gustan == null ? obj.Gustan : (int)dto.Gustan;
			obj.NoGustan = dto.NoGustan == null ? obj.NoGustan : (int)dto.NoGustan;
			obj.IdEstatusPublicacion = dto.IdEstatusPublicacion == null ? obj.IdEstatusPublicacion : (int)dto.IdEstatusPublicacion;
			obj.IdFasePublicacion = dto.IdFasePublicacion == null ? obj.IdFasePublicacion : (int)dto.IdFasePublicacion;
			obj.IdTipoPublicacion = dto.IdTipoPublicacion == null ? obj.IdTipoPublicacion : (int)dto.IdTipoPublicacion;
			obj.IdClasePublicacion = dto.IdClasePublicacion == null ? obj.IdClasePublicacion : (int)dto.IdClasePublicacion;
			obj.IdRevisadaPor = dto.IdRevisadaPor == null ? obj.IdRevisadaPor : (Guid?)dto.IdRevisadaPor;
			obj.IdImagenPrincipal = dto.IdImagenPrincipal == null ? obj.IdImagenPrincipal : (Guid?)dto.IdImagenPrincipal;
			obj.TiempoEstimado = dto.TiempoEstimado == null ? obj.TiempoEstimado : (int)dto.TiempoEstimado;
			obj.Posicionamiento = dto.Posicionamiento == null ? obj.Posicionamiento : (int)dto.Posicionamiento;
			obj.Secuencia = dto.Secuencia == null ? obj.Secuencia : (int)dto.Secuencia;
			obj.Vistas = dto.Vistas == null ? obj.Vistas : (int)dto.Vistas;
			obj.Evaluacion = dto.Evaluacion == null ? obj.Evaluacion : (decimal)dto.Evaluacion;
			obj.DireccionIpCreacion = dto.DireccionIpCreacion == null ? obj.DireccionIpCreacion : dto.DireccionIpCreacion;
			obj.Dispositivo = dto.Dispositivo == null ? obj.Dispositivo : dto.Dispositivo;
			obj.Direccion = dto.Direccion == null ? obj.Direccion : dto.Direccion;
			obj.ReferenciasDireccion = dto.ReferenciasDireccion == null ? obj.ReferenciasDireccion : dto.ReferenciasDireccion;
			obj.FechaDisponible = dto.FechaDisponible == null ? obj.FechaDisponible : (DateTime?)dto.FechaDisponible;
			obj.TotalArticulos = dto.TotalArticulos == null ? obj.TotalArticulos : (int)dto.TotalArticulos;
			obj.IdProyecto = dto.IdProyecto == null ? obj.IdProyecto : (Guid?)dto.IdProyecto;
			obj.CostoEstimado = dto.CostoEstimado == null ? obj.CostoEstimado : (decimal)dto.CostoEstimado;
			obj.IdMonedaCostoEstimado = dto.IdMonedaCostoEstimado == null ? obj.IdMonedaCostoEstimado : dto.IdMonedaCostoEstimado;
			obj.TipoCambioCostoEstimado = dto.TipoCambioCostoEstimado == null ? obj.TipoCambioCostoEstimado : (decimal)dto.TipoCambioCostoEstimado;
			obj.CostoReal = dto.CostoReal == null ? obj.CostoReal : (decimal)dto.CostoReal;
			obj.MontoInversion = dto.MontoInversion == null ? obj.MontoInversion : (decimal)dto.MontoInversion;
			obj.CostoRealTraslado = dto.CostoRealTraslado == null ? obj.CostoRealTraslado : (decimal)dto.CostoRealTraslado;
			obj.IdMonedaCostoReal = dto.IdMonedaCostoReal == null ? obj.IdMonedaCostoReal : dto.IdMonedaCostoReal;
			obj.TipoCambioCostoReal = dto.TipoCambioCostoReal == null ? obj.TipoCambioCostoReal : (decimal)dto.TipoCambioCostoReal;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }

    public async Task<bool> EliminarLotePublicacion(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Publicacion> objs = new List<Publicacion>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Publicaciones.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Publicaciones.UpdateRange(objs);

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
