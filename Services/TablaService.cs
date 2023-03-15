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

public class TablaService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorTablaRequeridos validadorReqs;
    private readonly ValidadorTabla validador;

    public TablaService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorTablaRequeridos validadorReqs, ValidadorTabla validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<string>> CrearTabla(List<TablaDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var nuevo in nuevos)
            {
                if (nuevo.Id == null || nuevo.Id == "")
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(nuevo, Operacion.Creacion);

                Tabla obj = new Tabla();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);

                var v = ctx.Tablas.Add(obj);
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

    public async Task<ICollection<string>> ModificarTabla(List<TablaDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Tabla> objs = new List<Tabla>();
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var modif in modifs)
            {
                if (modif.Id == null || modif.Id == "")
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Tablas.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Creacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Tablas.UpdateRange(objs);

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

    public async Task<ICollection<string>> EliminarTabla(List<string> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Tabla> objs = new List<Tabla>();
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var id in ids)
            {
                var buscado = await ctx.Tablas.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            ctx.Tablas.UpdateRange(objs);

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

    private void ValidarDatos(TablaDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(Tabla obj, TablaDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = dto.Id!;
			obj.Descripcion = dto.Descripcion!;
			obj.EsGenerica = (bool?)dto.EsGenerica!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.EsGenerica = dto.EsGenerica == null ? obj.EsGenerica : (bool?)dto.EsGenerica;
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
