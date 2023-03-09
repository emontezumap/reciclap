
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorVariosRequeridos : AbstractValidator<VariosDTO>
{
    public ValidadorVariosRequeridos()
    {
        RuleFor(c => c.IdTabla)
            .NotNull()
            .NotEmpty()
            .WithMessage("IdTabla: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Descripcion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descripcion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
