
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorVersionApiRequeridos : AbstractValidator<VersionApiDTO>
{
    public ValidadorVersionApiRequeridos()
    {
        RuleFor(c => c.Version)
            .NotNull()
            .NotEmpty()
            .WithMessage("Version: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.VigenteDesde)
            .NotNull()
            .WithMessage("VigenteDesde: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
