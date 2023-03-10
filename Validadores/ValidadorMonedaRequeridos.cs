
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorMonedaRequeridos : AbstractValidator<MonedaDTO>
{
    public ValidadorMonedaRequeridos()
    {
        RuleFor(c => c.Nombre)
            .NotNull()
            .NotEmpty()
            .WithMessage("Nombre: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Simbolo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Simbolo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TipoCambio)
            .NotNull()
            .WithMessage("TipoCambio: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
