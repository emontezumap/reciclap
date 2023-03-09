
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorComentarioRequeridos : AbstractValidator<ComentarioDTO>
{
    public ValidadorComentarioRequeridos()
    {
        RuleFor(c => c.IdChat)
            .NotNull()
            .WithMessage("IdChat: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdUsuario)
            .NotNull()
            .WithMessage("IdUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Texto)
            .NotNull()
            .NotEmpty()
            .WithMessage("Texto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
