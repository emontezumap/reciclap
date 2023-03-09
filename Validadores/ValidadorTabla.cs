
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorTabla : AbstractValidator<TablaDTO>
{
    public ValidadorTabla()
    {
        RuleFor(c => c.Descripcion)
            .MaximumLength(-1)
            .WithMessage("Descripcion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
