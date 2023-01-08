using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Entidades;
using Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class LoginController : ControllerBase
{
    private readonly LoginService loginSvc;
    private IConfiguration config;

    public LoginController(LoginService loginSvc, IConfiguration config)
    {
        this.loginSvc = loginSvc;
        this.config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        var usr = await loginSvc.BuscarUsuario(login);

        if (usr is null)
            return BadRequest(new { message = "No autorizado" });

        string jwt = GenerarToken(usr);

        return Ok(new { token = jwt });
    }

    private string GenerarToken(Usuario usr)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usr.Nombre),
            new Claim(ClaimTypes.Surname, usr.Apellido),
            new Claim(ClaimTypes.Email, usr.Email),
            new Claim("Grupo", usr.Grupo.Descripcion )
        };

        var clave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value)
        );
        var creds = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256Signature);
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
