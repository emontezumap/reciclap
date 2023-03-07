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

public class VariosService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public VariosService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<ICollection<int>> CrearVarios(List<VariosDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                ValidadorVarios vc = new ValidadorVarios(nuevo, Operacion.Creacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Varios obj = new Varios();
                    Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                    ctx.Varias.Add(obj);
                    codigos.Add(obj.Id);
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
            else
                throw (new Excepcionador(res)).ExcepcionDatosNoValidos();

            return codigos;
        }
    }

    public async Task<ICollection<int>> ModificarVarios(List<VariosDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<Varios> objs = new List<Varios>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorVarios vc = new ValidadorVarios(modif, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var obj = await ctx.Varias.FindAsync(modif.Id);

                    if (obj != null) {
                        Mapear(obj, modif, idUsr, Operacion.Modificacion);
                        objs.Add(obj);
                        codigos.Add(obj.Id);
                    }
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Varias.UpdateRange(objs);

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
            else
                throw (new Excepcionador(res)).ExcepcionDatosNoValidos();

            return codigos;
        }
    }

    public async Task<ICollection<int>> EliminarVarios(List<int> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Varios> objs = new List<Varios>();
        ICollection<int> codigos = new List<int>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
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

            if (objs.Count > 0)
            {
                ctx.Varias.UpdateRange(objs);

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

            return codigos;
        }
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
}
