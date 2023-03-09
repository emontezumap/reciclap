
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorVersionApi : AbstractValidator<VersionApiDTO>
{
    public ValidadorVersionApi()
    {
        RuleFor(c => c.Version)
            .MaximumLength(15)
            .WithMessage("Version: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());
    }
}
