using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class EstadoController : ControllerBase
{
    EstadoService svc;

    public EstadoController(EstadoService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<Estado>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Estado>> PorId(Guid id)
    {
        var usr = await svc.PorId(id);
        return usr is null ? NotFound() : usr;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Estado nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid id, Estado modif)
    {
        if (id != modif.Id) return BadRequest();

        var buscado = await svc.PorId(id);
        if (buscado is null) return NotFound();
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
}