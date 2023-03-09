
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorComentario : AbstractValidator<ComentarioDTO>
{
    public ValidadorComentario()
    {
        RuleFor(c => c.Texto)
            .MaximumLength(-1)
            .WithMessage("Texto: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
