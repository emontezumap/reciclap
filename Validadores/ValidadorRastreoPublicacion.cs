
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorRastreoPublicacion : AbstractValidator<RastreoPublicacionDTO>
{
    public ValidadorRastreoPublicacion()
    {
        RuleFor(c => c.TiempoEstimado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Comentarios)
            .MaximumLength(100)
            .WithMessage("Comentarios: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
