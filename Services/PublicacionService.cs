using Microsoft.EntityFrameworkCore;
using Entidades;
using DB;
using DTOs;
using System.Security.Claims;
using Herramientas;
using Validadores;
using FluentValidation.Results;

namespace Services;

[ExtendObjectType("Mutacion")]

public class PublicacionService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorPublicacionRequeridos validadorReqs;
    private readonly ValidadorPublicacion validador;

    public PublicacionService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorPublicacionRequeridos validadorReqs, ValidadorPublicacion validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<Guid>> CrearPublicacion(List<PublicacionDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                var sec = await ctx.Secuencias.Where(s => s.Prefijo == "PUB").SingleOrDefaultAsync();
                long n = 1L;

                if (sec != null)
                {
                    sec.Serie++;
                    n = sec.Serie;
                }

                Publicacion obj = new Publicacion();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
				obj.Consecutivo = n;
                var v = ctx.Publicaciones.Add(obj);
                codigos.Add(v.Entity.Id);
            }

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> ModificarPublicacion(List<PublicacionDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Publicacion> objs = new List<Publicacion>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Publicaciones.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Creacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Publicaciones.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<Guid>> EliminarPublicacion(List<Guid> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Publicacion> objs = new List<Publicacion>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var id in ids)
            {
                var buscado = await ctx.Publicaciones.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            ctx.Publicaciones.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
                ctx.Database.CommitTransaction();
            }
            catch (DbUpdateException ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                ctx.Database.RollbackTransaction();
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    private void ValidarDatos(PublicacionDTO dto, Operacion op = Operacion.Modificacion)
    {
        ValidationResult vr;

        if (op == Operacion.Creacion)
        {
            vr = validadorReqs.Validate(dto);

            if (!vr.IsValid)
                throw new GraphQLException(vr.ToString());
        }

        vr = validador.Validate(dto);

        if (!vr.IsValid)
            throw new GraphQLException(vr.ToString());
    }

    public void Mapear(Publicacion obj, PublicacionDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.Titulo = dto.Titulo!;
			obj.Descripcion = dto.Descripcion!;
			obj.Fecha = (DateTime)dto.Fecha!;
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
			obj.TotalArticulos = (decimal)dto.TotalArticulos!;
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
			obj.TotalArticulos = dto.TotalArticulos == null ? obj.TotalArticulos : (decimal)dto.TotalArticulos;
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

    private Guid AutenticarUsuario(ClaimsPrincipal claims)
    {
        try
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));
            return id;
        }
        catch (ArgumentNullException ex)
        {
            throw (new Excepcionador(ex)).ExcepcionAutenticacion();
        }
    }
}
