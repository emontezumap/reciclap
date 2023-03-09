
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorPublicacionRequeridos : AbstractValidator<PublicacionDTO>
{
    public ValidadorPublicacionRequeridos()
    {
        RuleFor(c => c.Titulo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Titulo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Descripcion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descripcion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Consecutivo)
            .NotNull()
            .WithMessage("Consecutivo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdPublicador)
            .NotNull()
            .WithMessage("IdPublicador: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Gustan)
            .NotNull()
            .WithMessage("Gustan: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.NoGustan)
            .NotNull()
            .WithMessage("NoGustan: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdEstatusPublicacion)
            .NotNull()
            .WithMessage("IdEstatusPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdFasePublicacion)
            .NotNull()
            .WithMessage("IdFasePublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoPublicacion)
            .NotNull()
            .WithMessage("IdTipoPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdClasePublicacion)
            .NotNull()
            .WithMessage("IdClasePublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TiempoEstimado)
            .NotNull()
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Posicionamiento)
            .NotNull()
            .WithMessage("Posicionamiento: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Secuencia)
            .NotNull()
            .WithMessage("Secuencia: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Vistas)
            .NotNull()
            .WithMessage("Vistas: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Evaluacion)
            .NotNull()
            .WithMessage("Evaluacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.DireccionIpCreacion)
            .NotNull()
            .NotEmpty()
            .WithMessage("DireccionIpCreacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Dispositivo)
            .NotNull()
            .NotEmpty()
            .WithMessage("Dispositivo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Direccion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Direccion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.ReferenciasDireccion)
            .NotNull()
            .NotEmpty()
            .WithMessage("ReferenciasDireccion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TotalArticulos)
            .NotNull()
            .WithMessage("TotalArticulos: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.CostoEstimado)
            .NotNull()
            .WithMessage("CostoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdMonedaCostoEstimado)
            .NotNull()
            .NotEmpty()
            .WithMessage("IdMonedaCostoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TipoCambioCostoEstimado)
            .NotNull()
            .WithMessage("TipoCambioCostoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.CostoReal)
            .NotNull()
            .WithMessage("CostoReal: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.MontoInversion)
            .NotNull()
            .WithMessage("MontoInversion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.CostoRealTraslado)
            .NotNull()
            .WithMessage("CostoRealTraslado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdMonedaCostoReal)
            .NotNull()
            .NotEmpty()
            .WithMessage("IdMonedaCostoReal: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TipoCambioCostoReal)
            .NotNull()
            .WithMessage("TipoCambioCostoReal: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
