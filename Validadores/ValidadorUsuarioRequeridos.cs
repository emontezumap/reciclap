
using DTOs;
using FluentValidation;
using Herramientas;

namespace Validadores;

public class ValidadorUsuarioRequeridos : AbstractValidator<UsuarioDTO>
{
    public ValidadorUsuarioRequeridos()
    {
        RuleFor(c => c.Nombre)
            .NotNull()
            .NotEmpty()
            .WithMessage("Nombre: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Apellido)
            .NotNull()
            .NotEmpty()
            .WithMessage("Apellido: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Direccion)
            .NotNull()
            .NotEmpty()
            .WithMessage("Direccion: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdCiudad)
            .NotNull()
            .WithMessage("IdCiudad: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Telefono)
            .NotNull()
            .NotEmpty()
            .WithMessage("Telefono: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.Clave)
            .NotNull()
            .NotEmpty()
            .WithMessage("Clave: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdGrupo)
            .NotNull()
            .WithMessage("IdGrupo: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        RuleFor(c => c.IdTipoUsuario)
            .NotNull()
            .WithMessage("IdTipoUsuario: " + CodigosError.ERR_CAMPO_REQUERIDO.ToString())
            .WithErrorCode(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
    }
}
