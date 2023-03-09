
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorBitacoraProyecto : AbstractValidator<BitacoraProyectoDTO>
{
    public ValidadorBitacoraProyecto()
    {
        RuleFor(c => c.Comentarios)
            .MaximumLength(-1)
            .WithMessage("Comentarios: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
