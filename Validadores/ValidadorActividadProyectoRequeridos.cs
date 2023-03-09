
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorActividadProyectoRequeridos : AbstractValidator<ActividadProyectoDTO>
{
    public ValidadorActividadProyectoRequeridos()
    {
        RuleFor(c => c.IdProyecto)
            .NotNull()
            .WithMessage("IdProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdRutaProyecto)
            .NotNull()
            .WithMessage("IdRutaProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Secuencia)
            .NotNull()
            .WithMessage("Secuencia: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdActividadRuta)
            .NotNull()
            .WithMessage("IdActividadRuta: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Descripcion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descripcion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.FechaInicio)
            .NotNull()
            .WithMessage("FechaInicio: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdEstatusPublicacion)
            .NotNull()
            .WithMessage("IdEstatusPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdEstatusProyecto)
            .NotNull()
            .WithMessage("IdEstatusProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoActividad)
            .NotNull()
            .WithMessage("IdTipoActividad: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TiempoEstimado)
            .NotNull()
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.ProgresoEstimado)
            .NotNull()
            .WithMessage("ProgresoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Evaluacion)
            .NotNull()
            .WithMessage("Evaluacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
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
