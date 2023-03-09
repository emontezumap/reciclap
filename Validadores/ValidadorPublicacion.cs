
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorPublicacion : AbstractValidator<PublicacionDTO>
{
    public ValidadorPublicacion()
    {
        RuleFor(c => c.Titulo)
            .MaximumLength(200)
            .WithMessage("Titulo: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Descripcion)
            .MaximumLength(-1)
            .WithMessage("Descripcion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Consecutivo)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Consecutivo: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

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

        RuleFor(c => c.Posicionamiento)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Posicionamiento: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Secuencia)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Secuencia: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Vistas)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Vistas: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.Evaluacion)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Evaluacion: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.DireccionIpCreacion)
            .MaximumLength(20)
            .WithMessage("DireccionIpCreacion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Dispositivo)
            .MaximumLength(1)
            .WithMessage("Dispositivo: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.Direccion)
            .MaximumLength(200)
            .WithMessage("Direccion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

        RuleFor(c => c.ReferenciasDireccion)
            .MaximumLength(100)
            .WithMessage("ReferenciasDireccion: " + CodigosError.ERR_CADENA_MUY_LARGA.ToString())
            .WithErrorCode(CodigosError.ERR_CADENA_MUY_LARGA.ToString());

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

        RuleFor(c => c.MontoInversion)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MontoInversion: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.CostoRealTraslado)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CostoRealTraslado: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());

        RuleFor(c => c.TipoCambioCostoReal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TipoCambioCostoReal: " + CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString())
            .WithErrorCode(CodigosError.ERR_VALOR_FUERA_DE_RANGO.ToString());
    }
}
