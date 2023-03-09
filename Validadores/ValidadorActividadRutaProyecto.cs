
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorActividadRutaProyecto : AbstractValidator<ActividadRutaProyectoDTO>
{
    public ValidadorActividadRutaProyecto()
    {
        RuleFor(c => c.Descripcion)
            .MaximumLength(450)
            .WithMessage("Descripcion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Secuencia)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Secuencia: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
