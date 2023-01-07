using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class PublicacionController : ControllerBase
{
    PublicacionService svc;

    public PublicacionController(PublicacionService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<Publicacion>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Publicacion>> PorId(Guid id)
    {
        var usr = await svc.PorId(id);
        return usr is null ? NotFound() : usr;
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Crear(Publicacion nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { id = nuevo.Id }, nuevo);
    }

    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid id, Publicacion modif)
    {
        if (id != modif.Id) return BadRequest();

        var buscado = await svc.PorId(id);
        if (buscado is null) return NotFound();
        await svc.Modificar(modif);
        return NoContent();
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var buscado = await svc.PorId(id);
        if (buscado is null) return NotFound();

        await svc.Eliminar(id);
        return Ok();
    }
}