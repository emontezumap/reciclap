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

public class ChatService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ChatService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<ICollection<Guid>> CrearChat(List<ChatDTO> nuevos, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var nuevo in nuevos)
            {
                ValidadorChat vc = new ValidadorChat(nuevo, Operacion.Creacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Chat obj = new Chat();
                    Mapear(obj, nuevo, idUsr, Operacion.Creacion);
                    ctx.Chats.Add(obj);
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

    public async Task<ICollection<Guid>> ModificarChat(List<ChatDTO> modifs, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        Dictionary<string, Dictionary<string, HashSet<string>>> res = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        ICollection<Chat> objs = new List<Chat>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorChat vc = new ValidadorChat(modif, Operacion.Modificacion, ctx);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    var obj = await ctx.Chats.FindAsync(modif.Id);

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
                ctx.Chats.UpdateRange(objs);

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

    public async Task<ICollection<Guid>> EliminarChat(List<Guid> ids, ClaimsPrincipal claims)
    {
        Guid idUsr = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Chat> objs = new List<Chat>();
        ICollection<Guid> codigos = new List<Guid>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Chats.FindAsync(id);

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
                ctx.Chats.UpdateRange(objs);

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

    public void Mapear(Chat obj, ChatDTO dto, Guid id, Operacion op)
    {
        if (op == Operacion.Creacion)
        {
			obj.Id = Guid.NewGuid();
			obj.IdPublicacion = (Guid)dto.IdPublicacion!;
			obj.Titulo = dto.Titulo!;
			obj.Fecha = (DateTime)dto.Fecha!;
			obj.IdCreador = id;
			obj.FechaCreacion = DateTime.UtcNow;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = (bool?)dto.Activo!;
        }
        else
        {
			obj.IdPublicacion = dto.IdPublicacion == null ? obj.IdPublicacion : (Guid)dto.IdPublicacion;
			obj.Titulo = dto.Titulo == null ? obj.Titulo : dto.Titulo;
			obj.Fecha = dto.Fecha == null ? obj.Fecha : (DateTime)dto.Fecha;
			obj.IdModificador = id;
			obj.FechaModificacion = DateTime.UtcNow;
			obj.Activo = dto.Activo == null ? obj.Activo : (bool?)dto.Activo;
        }
    }
}
