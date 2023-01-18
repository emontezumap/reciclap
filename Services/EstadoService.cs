using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;

namespace Services;

[ExtendObjectType("Mutacion")]
public class EstadoService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public EstadoService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Estado>> TodosLosEstados()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.ToListAsync<Estado>();
        }
    }

    public async Task<Estado?> UnEstado(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Estados.FindAsync(id);
        }
    }

    public async Task<Estado> CrearEstado(EstadoDTO nuevo)
    {
        Estado est = new Estado()
        {
            Activo = nuevo.Activo,
            FechaCreacion = DateTime.UtcNow,
            FechaModificacion = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            // IdCreador =
            // IdModificador = 
            IdPais = (Guid)nuevo.IdPais,
            Nombre = nuevo.Nombre
        };

        using (var ctx = ctxFactory.CreateDbContext())
        {
            ctx.Estados.Add(est);
            await ctx.SaveChangesAsync();
        }

        return est;
    }

    public async Task<bool> ModificarEstado(EstadoDTO modif)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            var buscado = await ctx.Estados.FindAsync(modif.Id);

            if (buscado != null)
            {
                buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                buscado.FechaCreacion = DateTime.UtcNow;
                buscado.FechaModificacion = DateTime.UtcNow;
                buscado.Id = Guid.NewGuid();
                // buscado.IdCreador =;
                // buscado.IdModificador = ;
                buscado.IdPais = modif.IdPais == null ? buscado.IdPais : (Guid)modif.IdPais;
                buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;

                await ctx.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }

    public async Task<bool> EliminarEstado(Guid id)
    {
        EstadoDTO est = new EstadoDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarEstado(est);
    }
}