
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorSecuencia : AbstractValidator<SecuenciaDTO>
{
    public ValidadorSecuencia()
    {
        RuleFor(c => c.Prefijo)
            .MaximumLength(10)
            .WithMessage("Prefijo: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Serie)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Serie: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Incremento)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Incremento: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
