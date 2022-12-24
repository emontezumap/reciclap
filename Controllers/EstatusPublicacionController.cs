using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class EstatusPublicacionController : ControllerBase
{
    EstatusPublicacionService svc;

    public EstatusPublicacionController(EstatusPublicacionService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<EstatusPublicacion>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstatusPublicacion>> PorId(Guid id)
    {
        var usr = await svc.PorId(id);
        return usr is null ? NotFound() : usr;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(EstatusPublicacion nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid id, EstatusPublicacion modif)
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