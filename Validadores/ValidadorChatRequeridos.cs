
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorChatRequeridos : AbstractValidator<ChatDTO>
{
    public ValidadorChatRequeridos()
    {
        RuleFor(c => c.IdPublicacion)
            .NotNull()
            .WithMessage("IdPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Titulo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Titulo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
