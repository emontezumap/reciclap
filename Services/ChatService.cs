
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

    public async Task<Chat> CrearChat(ChatDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorChat vc = new ValidadorChat(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                Chat obj = new Chat();
                Mapear(obj, nuevo, id, Operacion.Creacion);

                try
                {
                    ctx.Chats.Add(obj);
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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> CrearLoteChat(List<ChatDTO> nuevos, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));

            foreach (var nuevo in nuevos)
            {
                ValidadorChat vc = new ValidadorChat(nuevo, Operacion.Creacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Chat obj = new Chat();
                    Mapear(obj, nuevo, id, Operacion.Creacion);
                    ctx.Chats.Add(obj);
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

    public async Task<bool> ModificarChat(ChatDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorChat vc = new ValidadorChat(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Chats.FindAsync(modif.Id);

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

    public async Task<Dictionary<string, Dictionary<string, HashSet<CodigosError>>>> ModificarLoteChat(List<ChatDTO> modifs, ClaimsPrincipal claims)
    {
        Dictionary<string, Dictionary<string, HashSet<CodigosError>>> res = new Dictionary<string, Dictionary<string, HashSet<CodigosError>>>();
        Guid id = Guid.Parse(claims.FindFirstValue("Id"));
        ICollection<Chat> objs = new List<Chat>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var modif in modifs)
            {
                ValidadorChat vc = new ValidadorChat(modif, Operacion.Modificacion, ctx, true);
                ResultadoValidacion rv = await vc.Validar();

                if (rv.ValidacionOk)
                {
                    Chat obj = new Chat();
                    Mapear(obj, modif, id, Operacion.Modificacion);
                    objs.Add(obj);
                }
                else
                    res.Add((modif.Id.ToString())!, rv.Mensajes!);
            }

            if (res.Count == 0)
            {
                ctx.Chats.UpdateRange(objs);
                await ctx.SaveChangesAsync();
            }
            return res;
        }
    }

    public async Task<bool> EliminarChat(Guid id, ClaimsPrincipal claims)
    {
        ChatDTO pub = new ChatDTO()
        {
			Id = id,

            Activo = false
        };

        return await ModificarChat(pub, claims);
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

    public async Task<bool> EliminarLoteChat(List<Guid> ids, ClaimsPrincipal claims)
    {
        ICollection<Chat> objs = new List<Chat>();

        using (var ctx = ctxFactory.CreateDbContext())
        {
            foreach (var id in ids)
            {
                var buscado = await ctx.Chats.FindAsync(id);

                if (buscado != null)
                {
                    buscado.Activo = false;
                    objs.Add(buscado);
                }
            }

            if (objs.Count > 0)
            {
                ctx.Chats.UpdateRange(objs);

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
