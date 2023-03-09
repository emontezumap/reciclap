
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorProyectoRequeridos : AbstractValidator<ProyectoDTO>
{
    public ValidadorProyectoRequeridos()
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

        RuleFor(c => c.FechaInicio)
            .NotNull()
            .WithMessage("FechaInicio: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdGerente)
            .NotNull()
            .WithMessage("IdGerente: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
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

        RuleFor(c => c.IdEstatusProyecto)
            .NotNull()
            .WithMessage("IdEstatusProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoProyecto)
            .NotNull()
            .WithMessage("IdTipoProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TiempoEstimado)
            .NotNull()
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.ProgresoEstimado)
            .NotNull()
            .WithMessage("ProgresoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.ProgresoReal)
            .NotNull()
            .WithMessage("ProgresoReal: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Evaluacion)
            .NotNull()
            .WithMessage("Evaluacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdRutaProyecto)
            .NotNull()
            .WithMessage("IdRutaProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
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
