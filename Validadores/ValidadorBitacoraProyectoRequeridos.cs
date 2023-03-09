
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorBitacoraProyectoRequeridos : AbstractValidator<BitacoraProyectoDTO>
{
    public ValidadorBitacoraProyectoRequeridos()
    {
        RuleFor(c => c.IdProyecto)
            .NotNull()
            .WithMessage("IdProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdActividadProyecto)
            .NotNull()
            .WithMessage("IdActividadProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Fecha)
            .NotNull()
            .WithMessage("Fecha: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdUsuario)
            .NotNull()
            .WithMessage("IdUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoBitacora)
            .NotNull()
            .WithMessage("IdTipoBitacora: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Comentarios)
            .NotNull()
            .NotEmpty()
            .WithMessage("Comentarios: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
