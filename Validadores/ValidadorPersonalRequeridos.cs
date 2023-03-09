
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorPersonalRequeridos : AbstractValidator<PersonalDTO>
{
    public ValidadorPersonalRequeridos()
    {
        RuleFor(c => c.IdPublicacion)
            .NotNull()
            .WithMessage("IdPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdUsuario)
            .NotNull()
            .WithMessage("IdUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdRol)
            .NotNull()
            .WithMessage("IdRol: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
