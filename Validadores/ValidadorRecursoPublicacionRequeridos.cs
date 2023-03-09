
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorRecursoPublicacionRequeridos : AbstractValidator<RecursoPublicacionDTO>
{
    public ValidadorRecursoPublicacionRequeridos()
    {
        RuleFor(c => c.IdTipoCatalogo)
            .NotNull()
            .WithMessage("IdTipoCatalogo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdCatalogo)
            .NotNull()
            .WithMessage("IdCatalogo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Secuencia)
            .NotNull()
            .WithMessage("Secuencia: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoRecurso)
            .NotNull()
            .WithMessage("IdTipoRecurso: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdUsuario)
            .NotNull()
            .WithMessage("IdUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Orden)
            .NotNull()
            .WithMessage("Orden: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Nombre)
            .NotNull()
            .NotEmpty()
            .WithMessage("Nombre: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdEstatusRecurso)
            .NotNull()
            .WithMessage("IdEstatusRecurso: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Tamano)
            .NotNull()
            .WithMessage("Tamano: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
