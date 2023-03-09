
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorRecursoPublicacion : AbstractValidator<RecursoPublicacionDTO>
{
    public ValidadorRecursoPublicacion()
    {
        RuleFor(c => c.Secuencia)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Secuencia: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Orden)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Orden: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Nombre)
            .MaximumLength(100)
            .WithMessage("Nombre: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Tamano)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tamano: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
