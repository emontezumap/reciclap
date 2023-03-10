
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorAdministrador : AbstractValidator<AdministradorDTO>
{
    public ValidadorAdministrador()
    {
        RuleFor(c => c.Nombre)
            .MaximumLength(50)
            .WithMessage("Nombre: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Apellido)
            .MaximumLength(50)
            .WithMessage("Apellido: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Telefono)
            .MaximumLength(20)
            .WithMessage("Telefono: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Email)
            .MaximumLength(300)
            .WithMessage("Email: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Clave)
            .MaximumLength(256)
            .WithMessage("Clave: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
