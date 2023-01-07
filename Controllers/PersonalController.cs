using Microsoft.AspNetCore.Mvc;
using Entidades;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class PersonalController : ControllerBase
{
    PersonalService svc;

    public PersonalController(PersonalService svc)
    {
        this.svc = svc;
    }

    [HttpGet]
    public async Task<IEnumerable<Personal>> Todos()
    {
        return await svc.Todos();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Personal>> PorId(Guid idPub, Guid idUsr)
    {
        var buscado = await svc.PorId(idPub, idUsr);
        return buscado is null ? NotFound() : buscado;
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Crear(Personal nuevo)
    {
        var creado = await svc.Crear(nuevo);
        return CreatedAtAction(nameof(PorId), new { IdPublicacion = nuevo.IdPublicacion, IdUsuario = nuevo.IdUsuario }, nuevo);
    }

    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Modificar(Guid idPub, Guid idUsr, Personal modif)
    {
        if (idPub != modif.IdPublicacion || idUsr != modif.IdUsuario) return BadRequest();

        var buscado = await svc.PorId(idPub, idUsr);
        if (buscado is null) return NotFound();
        await svc.Modificar(modif);
        return NoContent();
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid idPub, Guid idUsr)
    {
        var buscado = await svc.PorId(idPub, idUsr);
        if (buscado is null) return NotFound();

        await svc.Eliminar(idPub, idUsr);
        return Ok();
    }
}