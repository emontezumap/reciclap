
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorProyecto : AbstractValidator<ProyectoDTO>
{
    public ValidadorProyecto()
    {
        RuleFor(c => c.Titulo)
            .MaximumLength(450)
            .WithMessage("Titulo: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Descripcion)
            .MaximumLength(-1)
            .WithMessage("Descripcion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Gustan)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Gustan: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.NoGustan)
            .GreaterThanOrEqualTo(0)
            .WithMessage("NoGustan: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.TiempoEstimado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.ProgresoEstimado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ProgresoEstimado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.ProgresoReal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ProgresoReal: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Evaluacion)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Evaluacion: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.TotalArticulos)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TotalArticulos: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.CostoEstimado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CostoEstimado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.TipoCambioCostoEstimado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TipoCambioCostoEstimado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.CostoReal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CostoReal: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.TipoCambioCostoReal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TipoCambioCostoReal: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
