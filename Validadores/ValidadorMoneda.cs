
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorMoneda : AbstractValidator<MonedaDTO>
{
    public ValidadorMoneda()
    {
        RuleFor(c => c.Nombre)
            .MaximumLength(20)
            .WithMessage("Nombre: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.TipoCambio)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TipoCambio: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
