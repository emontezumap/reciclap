
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorSecuenciaRequeridos : AbstractValidator<SecuenciaDTO>
{
    public ValidadorSecuenciaRequeridos()
    {
        RuleFor(c => c.Prefijo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Prefijo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Serie)
            .NotNull()
            .WithMessage("Serie: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Incremento)
            .NotNull()
            .WithMessage("Incremento: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
