
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorVarios : AbstractValidator<VariosDTO>
{
    public ValidadorVarios()
    {
        RuleFor(c => c.Descripcion)
            .MaximumLength(1000)
            .WithMessage("Descripcion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Referencia)
            .MaximumLength(50)
            .WithMessage("Referencia: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
