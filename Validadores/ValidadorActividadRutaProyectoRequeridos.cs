
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorActividadRutaProyectoRequeridos : AbstractValidator<ActividadRutaProyectoDTO>
{
    public ValidadorActividadRutaProyectoRequeridos()
    {
        RuleFor(c => c.IdRutaProyecto)
            .NotNull()
            .WithMessage("IdRutaProyecto: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Descripcion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descripcion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Secuencia)
            .NotNull()
            .WithMessage("Secuencia: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
