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

public class MonedaService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;
    private readonly ValidadorMonedaRequeridos validadorReqs;
    private readonly ValidadorMoneda validador;

    public MonedaService(IDbContextFactory<SSDBContext> ctxFactory, ValidadorMonedaRequeridos validadorReqs, ValidadorMoneda validador)
    {
        this.ctxFactory = ctxFactory;
        this.validadorReqs = validadorReqs;
        this.validador = validador;
    }

    public async Task<ICollection<string>> CrearMoneda(List<MonedaDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                if (nuevo.Id == null || nuevo.Id == "")
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(nuevo, Operacion.Creacion);

                Moneda obj = new Moneda();
                Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                var v = ctx.Monedas.Add(obj);
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

    public async Task<ICollection<string>> ModificarMoneda(List<MonedaDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Moneda> objs = new List<Moneda>();
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                if (modif.Id == null || modif.Id == "")
                    throw new GraphQLException("Id: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString());

                ValidarDatos(modif);

                var obj = await ctx.Monedas.FindAsync(modif.Id);

                if (obj != null) {
                    Mapear(obj, modif, idUsr, Operacion.Modificacion);
                    objs.Add(obj);
                    codigos.Add(obj.Id);
                }
            }

            ctx.Monedas.UpdateRange(objs);

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

    public async Task<ICollection<string>> EliminarMoneda(List<string> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = AutenticarUsuario(claims);
        ICollection<Moneda> objs = new List<Moneda>();
        ICollection<string> codigos = new List<string>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Monedas.FindAsync(id);

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
                ctx.Monedas.UpdateRange(objs);

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

    private void ValidarDatos(MonedaDTO dto, Operacion op = Operacion.Modificacion)
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

    public void Mapear(Moneda obj, MonedaDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = dto.Id!;
			obj.Nombre = dto.Nombre!;
			obj.Simbolo = dto.Simbolo!;
			obj.TipoCambio = (decimal)dto.TipoCambio!;
			obj.EsLocal = (bool?)dto.EsLocal!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.Nombre = dto.Nombre == null ? obj.Nombre : dto.Nombre;
			obj.Simbolo = dto.Simbolo == null ? obj.Simbolo : dto.Simbolo;
			obj.TipoCambio = dto.TipoCambio == null ? obj.TipoCambio : (decimal)dto.TipoCambio;
			obj.EsLocal = dto.EsLocal == null ? obj.EsLocal : (bool?)dto.EsLocal;
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
