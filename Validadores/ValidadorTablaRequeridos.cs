
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorTablaRequeridos : AbstractValidator<TablaDTO>
{
    public ValidadorTablaRequeridos()
    {
        RuleFor(c => c.Descripcion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descripcion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
