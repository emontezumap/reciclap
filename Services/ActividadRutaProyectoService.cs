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

public class ActividadRutaProyectoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorActividadRutaProyectoRequeridos validadorReqs;
    private readonly ValidadorActividadRutaProyecto validador;

    public ActividadRutaProyectoService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorActividadRutaProyectoRequeridos validadorReqs, ValidadorActividadRutaProyecto validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<int>> CrearActividadRutaProyecto(List<ActividadRutaProyectoDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                ActividadRutaProyecto obj = new ActividadRutaProyecto();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.ActividadesRutasProyectos.Add(obj);
                codigos.Add(v.Entity.Id);
            }

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<int>> ModificarActividadRutaProyecto(List<ActividadRutaProyectoDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<ActividadRutaProyecto> objs = new List<ActividadRutaProyecto>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.ActividadesRutasProyectos.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Modificacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.ActividadesRutasProyectos.UpdateRange(objs);

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }
            catch (Exception ex)
            {
                throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
            }

            return codigos;
        }
    }

    public async Task<ICollection<int>> EliminarActividadRutaProyecto(List<int> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<ActividadRutaProyecto> objs = new List<ActividadRutaProyecto>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.ActividadesRutasProyectos.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            if (objs.Count > 0)
            {
                ctx.ActividadesRutasProyectos.UpdateRange(objs);

                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
                }
                catch (Exception ex)
                {
                    throw (new Excepcionador(ex)).ProcesarExcepcionActualizacionDB();
                }
            }

            return codigos;
        }
    }

    private void ValidarDatos(ActividadRutaProyectoDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(ActividadRutaProyecto obj, ActividadRutaProyectoDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdRutaProyecto = (int)dto.IdRutaProyecto!;
			obj.Descripcion = dto.Descripcion!;
			obj.Secuencia = (int)dto.Secuencia!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdRutaProyecto = dto.IdRutaProyecto == null ? obj.IdRutaProyecto : (int)dto.IdRutaProyecto;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.Secuencia = dto.Secuencia == null ? obj.Secuencia : (int)dto.Secuencia;
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
