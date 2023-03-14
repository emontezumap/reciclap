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

public class VariosService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorVariosRequeridos validadorReqs;
    private readonly ValidadorVarios validador;

    public VariosService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorVariosRequeridos validadorReqs, ValidadorVarios validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<int>> CrearVarios(List<VariosDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var nuevo in nuevos)
            {

                ValidarDatos(nuevo, Operacion.Creacion);

                Varios obj = new Varios();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.Varias.Add(obj);
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

    public async Task<ICollection<int>> ModificarVarios(List<VariosDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Varios> objs = new List<Varios>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var modif in modifs)
            {
                if (modif.Id == null)
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Varias.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Creacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Varias.UpdateRange(objs);

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

    public async Task<ICollection<int>> EliminarVarios(List<int> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Varios> objs = new List<Varios>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Database.BeginTransaction();

            foreach (var id in ids)
            {
                var buscado = await ctx.Varias.FindAsync(id);

                if (buscado != null)
                {

                    buscado.IdModificador = idUsr;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.Activo = false;

                    objs.Add(buscado);
                    codigos.Add(buscado.Id);
                }
            }

            ctx.Varias.UpdateRange(objs);

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

    private void ValidarDatos(VariosDTO dto, Operacion op = Operacion.Modificacion)
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

	public void Mapear(Varios obj, VariosDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.IdTabla = dto.IdTabla!;
			obj.Descripcion = dto.Descripcion!;
			obj.Referencia = (string?)dto.Referencia!;
			obj.IdPadre = (int?)dto.IdPadre!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdTabla = dto.IdTabla == null ? obj.IdTabla : dto.IdTabla;
			obj.Descripcion = dto.Descripcion == null ? obj.Descripcion : dto.Descripcion;
			obj.Referencia = dto.Referencia == null ? obj.Referencia : (string?)dto.Referencia;
			obj.IdPadre = dto.IdPadre == null ? obj.IdPadre : (int?)dto.IdPadre;
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
