using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Entidades;
using Services;
using DTOs;

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
        string jwt;

        if (usr != null)
            jwt = GenerarToken(usr);
        else
        {
            var adm = await loginSvc.BuscarAdministrador(login);

            if (adm == null)
                return BadRequest(new { message = "No autorizado" });

            jwt = GenerarToken(adm);
        }

        return Ok(new { token = jwt });
    }

    private string GenerarToken(Usuario usr)
    {
        var claims = new[]
        {
            new Claim("Id", usr.Id.ToString()),
            new Claim(ClaimTypes.Email, usr.Email!),
            new Claim("Grupo", usr.Grupo!.Descripcion ),
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

    private string GenerarToken(Administrador adm)
    {
        var claims = new[]
        {
            new Claim("Id", adm.Id.ToString()),
            new Claim(ClaimTypes.Email, adm.Email!),
            new Claim("Grupo", adm.Grupo!.Descripcion ),
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
