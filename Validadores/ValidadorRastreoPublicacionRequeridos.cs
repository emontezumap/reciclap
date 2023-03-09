
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorRastreoPublicacionRequeridos : AbstractValidator<RastreoPublicacionDTO>
{
    public ValidadorRastreoPublicacionRequeridos()
    {
        RuleFor(c => c.IdPublicacion)
            .NotNull()
            .WithMessage("IdPublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdFasePublicacion)
            .NotNull()
            .WithMessage("IdFasePublicacion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdUsuario)
            .NotNull()
            .WithMessage("IdUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.TiempoEstimado)
            .NotNull()
            .WithMessage("TiempoEstimado: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Comentarios)
            .NotNull()
            .NotEmpty()
            .WithMessage("Comentarios: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
