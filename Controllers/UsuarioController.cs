using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    UsuarioService svc;

    public UsuarioController(UsuarioService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<Usuario>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> PorId(Guid id)
    {
        var usr = await svc.PorId(id);
        return usr is null ? UsuarioNoExiste(id) : usr;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Usuario nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid id, Usuario modif)
    {
        if (id != modif.Id)
            return BadRequest(new
            {
                message = $"El id de usuario especificado ({id}) para modificar no coincide con el id de usuario ({modif.Id}) del objeto modificado"
            });

        var buscado = await svc.PorId(id);
        if (buscado is null) return UsuarioNoExiste(id);
        await svc.Modificar(modif);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var buscado = await svc.PorId(id);
        if (buscado is null) return NotFound();

        await svc.Eliminar(id);
        return Ok();
    }

    public NotFoundObjectResult UsuarioNoExiste(Guid id)
    {
        return NotFound(new
        {
            message = $"El id de usuario {id} no existe en la base de datos"
        });
    }
}