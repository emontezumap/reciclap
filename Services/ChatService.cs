using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using System.Security.Claims;
// using HotChocolate.AspNetCore.Authorization;
using Herramientas;
using Validadores;
using DB;
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

    public async Task<IEnumerable<Chat>> TodosLosChats()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Chats.ToListAsync<Chat>();
        }
    }

    public async Task<Chat?> UnChat(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Chats.FindAsync(id);
        }
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
                Chat chat = new Chat()
                {
                    Activo = nuevo.Activo,
                    Fecha = (DateTime)nuevo.Fecha!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdModificador = id,
                    IdPublicacion = (Guid)nuevo.IdPublicacion!,
                    Titulo = nuevo.Titulo!
                };

                try
                {
                    ctx.Chats.Add(chat);
                    await ctx.SaveChangesAsync();

                    return chat;
                }
                catch (DbUpdateException ex)
                {
                    throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "El chat");
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

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdPublicacion = modif.IdPublicacion == null ? buscado.IdPublicacion : (Guid)modif.IdPublicacion;
                    buscado.IdModificador = id;
                    buscado.Titulo = modif.Titulo == null ? buscado.Titulo : modif.Titulo;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "El chat");
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

    public async Task<bool> EliminarChat(Guid id, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            try
            {
                Chat o = new Chat() { Id = id };
                ctx.Chats.Remove(o);
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex, "El chat");
            }
            catch (Exception ex)
            {
                throw (new Excepcionador()).ProcesarExcepcionActualizacionDB(ex);
            }
        }

        // ChatDTO chat = new ChatDTO()
        // {
        //     Id = id,
        //     Activo = false
        // };

        // return await ModificarChat(chat, claims);
    }
}
