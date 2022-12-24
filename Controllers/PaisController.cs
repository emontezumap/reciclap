using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class PaisController : ControllerBase
{
    PaisService svc;

    public PaisController(PaisService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<Pais>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pais>> PorId(Guid id)
    {
        var usr = await svc.PorId(id);
        return usr is null ? NotFound() : usr;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Pais nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid id, Pais modif)
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