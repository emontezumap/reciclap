
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorChat : AbstractValidator<ChatDTO>
{
    public ValidadorChat()
    {
        RuleFor(c => c.Titulo)
            .MaximumLength(300)
            .WithMessage("Titulo: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
