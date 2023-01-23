using Microsoft.EntityFrameworkCore;
using DTOs;
using Entidades;
using System.Security.Claims;
using Herramientas;
using Validadores;

namespace Services;

[ExtendObjectType("Mutacion")]
public class ComentarioService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public ComentarioService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Comentario>> TodosLosComentarios()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Comentarios.ToListAsync<Comentario>();
        }
    }

    public async Task<Comentario?> UnComentario(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Comentarios.FindAsync(id);
        }
    }

    public async Task<Comentario> CrearComentario(ComentarioDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));
            ValidadorComentario vc = new ValidadorComentario(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Comentario com = new Comentario()
                {
                    Activo = nuevo.Activo,
                    Fecha = (DateTime)nuevo.Fecha!,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdChat = (Guid)nuevo.IdChat!,
                    IdComentario = (Guid)nuevo.IdComentario!,
                    IdCreador = id,
                    IdModificador = id,
                    IdUsuario = (Guid)nuevo.IdUsuario!,
                    Texto = nuevo.Texto!,
                };

                ctx.Comentarios.Add(com);
                await ctx.SaveChangesAsync();

                return com;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarComentario(ComentarioDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorComentario vc = new ValidadorComentario(modif, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                var buscado = await ctx.Comentarios.FindAsync(modif.Id);

                if (buscado != null)
                {
                    Guid id = Guid.Parse(claims.FindFirstValue("Id"));

                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.Fecha = modif.Fecha == null ? buscado.Fecha : (DateTime)modif.Fecha;
                    buscado.FechaModificacion = buscado.FechaCreacion;
                    buscado.IdChat = modif.IdChat == null ? buscado.IdChat : (Guid)modif.IdChat;
                    buscado.IdComentario = modif.IdComentario == null ? buscado.IdComentario : (Guid)modif.IdComentario;
                    buscado.IdModificador = id;
                    buscado.IdUsuario = modif.IdUsuario == null ? buscado.IdUsuario : (Guid)modif.IdUsuario;
                    buscado.Texto = modif.Texto == null ? buscado.Texto : modif.Texto;

                    await ctx.SaveChangesAsync();
                    return true;
                }
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }

        return false;
    }

    public async Task<bool> EliminarComentario(Guid id, ClaimsPrincipal claims)
    {
        ComentarioDTO com = new ComentarioDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarComentario(com, claims);
    }
}