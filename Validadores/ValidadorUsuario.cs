
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorUsuario : AbstractValidator<UsuarioDTO>
{
    public ValidadorUsuario()
    {
        RuleFor(c => c.Nombre)
            .MaximumLength(100)
            .WithMessage("Nombre: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Apellido)
            .MaximumLength(100)
            .WithMessage("Apellido: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.SegundoNombre)
            .MaximumLength(50)
            .WithMessage("SegundoNombre: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.SegundoApellido)
            .MaximumLength(50)
            .WithMessage("SegundoApellido: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Perfil)
            .MaximumLength(200)
            .WithMessage("Perfil: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Direccion)
            .MaximumLength(300)
            .WithMessage("Direccion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Telefono)
            .MaximumLength(20)
            .WithMessage("Telefono: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Telefono2)
            .MaximumLength(20)
            .WithMessage("Telefono2: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Email)
            .MaximumLength(300)
            .WithMessage("Email: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Clave)
            .MaximumLength(256)
            .WithMessage("Clave: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Email2)
            .MaximumLength(250)
            .WithMessage("Email2: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.MaximoPublicaciones)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MaximoPublicaciones: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Estatus)
            .MaximumLength(2)
            .WithMessage("Estatus: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.UltimaIp)
            .MaximumLength(20)
            .WithMessage("UltimaIp: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
