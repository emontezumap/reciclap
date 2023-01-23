using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Entidades;
using DTOs;
using HotChocolate.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Validadores;
using Herramientas;

namespace Services;

[ExtendObjectType("Mutacion")]
public class CiudadService
{
    private readonly IDbContextFactory<SSDBContext> ctxFactory;

    public CiudadService(IDbContextFactory<SSDBContext> ctxFactory)
    {
        this.ctxFactory = ctxFactory;
    }

    public async Task<IEnumerable<Ciudad>> TodasLasCiudades()
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.ToListAsync<Ciudad>();
        }
    }

    public async Task<Ciudad?> UnaCiudad(Guid id)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            return await ctx.Ciudades.FindAsync(id);
        }
    }

    public async Task<Ciudad> CrearCiudad(CiudadDTO nuevo, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            Guid id = Guid.Parse(claims.FindFirstValue("Id"));
            ValidadorCiudad vc = new ValidadorCiudad(nuevo, Operacion.Creacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Ciudad ciudad = new Ciudad()
                {
                    Activo = nuevo.Activo,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IdCreador = id,
                    IdEstado = (Guid)nuevo.IdEstado!,
                    IdModificador = id,
                    Nombre = nuevo.Nombre!
                };

                ctx.Ciudades.Add(ciudad);
                await ctx.SaveChangesAsync();

                return ciudad;
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }
    }

    public async Task<bool> ModificarCiudad(CiudadDTO modif, ClaimsPrincipal claims)
    {
        using (var ctx = ctxFactory.CreateDbContext())
        {
            ValidadorCiudad vc = new ValidadorCiudad(modif, Operacion.Modificacion, ctx);
            ResultadoValidacion rv = await vc.Validar();

            if (rv.ValidacionOk)
            {
                Guid id = Guid.Parse(claims.FindFirstValue("Id"));
                var buscado = await ctx.Ciudades.FindAsync(modif.Id);

                if (buscado != null)
                {
                    buscado.Activo = modif.Activo == null ? buscado.Activo : modif.Activo;
                    buscado.FechaModificacion = DateTime.UtcNow;
                    buscado.IdEstado = modif.IdEstado == null ? buscado.IdEstado : (Guid)modif.IdEstado;
                    buscado.IdModificador = id;
                    buscado.Nombre = modif.Nombre == null ? buscado.Nombre : modif.Nombre;

                    await ctx.SaveChangesAsync();
                    return true;
                }
            }
            else
                throw (new Excepcionador(rv)).ExcepcionDatosNoValidos();
        }

        return false;
    }

    public async Task<bool> EliminarCiudad(Guid id, ClaimsPrincipal claims)
    {
        CiudadDTO ciudad = new CiudadDTO()
        {
            Id = id,
            Activo = false
        };

        return await ModificarCiudad(ciudad, claims);
    }
}