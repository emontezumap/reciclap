using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorUsuario : IValidadorEntidad
{
    private Dictionary<string, List<string>> mensajes;
    private UsuarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    public bool Ok { get; set; } = false;

    public ValidadorUsuario(UsuarioDTO dto, Operacion op, SSDBContext ctx)
    {
        mensajes = new Dictionary<string, List<string>>() {
            { "Id", new List<string>() },
            { "Nombre", new List<string>()},
            { "Nombre2", new List<string>()},
            { "Apellido", new List<string>()},
            { "Apellido2", new List<string>()},
            { "Perfil", new List<string>()},
            { "Direccion", new List<string>()},
            { "IdCiudad", new List<string>()},
            { "Telefono", new List<string>() },
            { "Telefono2", new List<string>()},
            { "Email", new List<string>()},
            { "Clave", new List<string>()},
            { "Email2", new List<string>()},
            { "IdProfesion", new List<string>()},
            { "MaximoPublicaciones", new List<string>()},
            { "IdGrupo", new List<string>()},
            { "Activo", new List<string>()}
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
    }

    public async Task<ResultadoValidacion> Validar()
    {
        bool hayError = false;

        if (op == Operacion.Modificacion && await ctx.Usuarios.FindAsync(dto.Id) == null)
        {
            mensajes["Id"].Add("El usuario especificado no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Nombre))
            if (op == Operacion.Creacion)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del usuario");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Nombre"].Add("Se requiere el nombre del usuario");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Nombre) && dto.Nombre.Length > 50)
        {
            mensajes["Nombre"].Add("El nombre del usuario no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (!string.IsNullOrEmpty(dto.Nombre2) && dto.Nombre2.Length > 50)
        {
            mensajes["Nombre2"].Add("El segundo nombre del usuario no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Apellido))
            if (op == Operacion.Creacion)
            {
                mensajes["Apellido"].Add("Se requiere el apellido del usuario");
                hayError = true;
            }
            else if (dto.Nombre != null)
            {
                mensajes["Apellido"].Add("Se requiere el apellido del usuario");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Apellido) && dto.Apellido.Length > 50)
        {
            mensajes["Apellido"].Add("El apellido del usuario no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (!string.IsNullOrEmpty(dto.Apellido2) && dto.Apellido2.Length > 50)
        {
            mensajes["Apellido2"].Add("El segundo apellido del usuario no debe exceder los 50 caracteres");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Direccion))
            if (op == Operacion.Creacion)
            {
                mensajes["Direccion"].Add("Se requiere una dirección");
                hayError = true;
            }
            else if (dto.Direccion != null)
            {
                mensajes["Direccion"].Add("Se requiere una dirección");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Direccion) && dto.Direccion.Length > 300)
        {
            mensajes["Direccion"].Add("La dirección del usuario no debe exceder los 300 caracteres");
            hayError = true;
        }

        if (dto.IdCiudad == null)
        {
            mensajes["IdCiudad"].Add("Se requiere una ciudad");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.Ciudades.FindAsync(dto.IdCiudad) == null)
        {
            mensajes["IdCiudad"].Add("La Ciudad especificada no existe");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Telefono))
            if (op == Operacion.Creacion)
            {
                mensajes["Telefono"].Add("Se requiere un número telefónico");
                hayError = true;
            }
            else if (dto.Telefono != null)
            {
                mensajes["Telefono"].Add("Se requiere un número telefónico");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Telefono) && dto.Telefono.Length > 20)
        {
            mensajes["Telefono"].Add("El número telefónico no debe exceder los 20 caracteres");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Email))
            if (op == Operacion.Creacion)
            {
                mensajes["Email"].Add("Se requiere una dirección de correo electrónico");
                hayError = true;
            }
            else if (dto.Email != null)
            {
                mensajes["Email"].Add("Se requiere una dirección de correo electrónico");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Email) && dto.Email.Length > 250)
        {
            mensajes["Email"].Add("El número telefónico no debe exceder los 250 caracteres");
            hayError = true;
        }

        if (string.IsNullOrEmpty(dto.Clave))
            if (op == Operacion.Creacion)
            {
                mensajes["Clave"].Add("Se requiere una clave");
                hayError = true;
            }
            else if (dto.Clave != null)
            {
                mensajes["Clave"].Add("Se requiere una clave");
                hayError = true;
            }

        if (!string.IsNullOrEmpty(dto.Clave) && (dto.Clave.Length < 8 || dto.Clave.Length > 256))
        {
            mensajes["Clave"].Add("La clave debe tener entre 8 y 256 caracteres");
            hayError = true;
        }

        if (op == Operacion.Modificacion && await ctx.Profesiones.FindAsync(dto.Id) == null)
        {
            mensajes["IdProfesion"].Add("La profesión especificada no existe");
            hayError = true;
        }

        if (op == Operacion.Creacion && dto.IdGrupo == null)
        {
            mensajes["IdGrupo"].Add("Se requiere un grupo de usuarios");
            hayError = true;
        }
        else if (op == Operacion.Modificacion && await ctx.Grupos.FindAsync(dto.IdGrupo) == null)
        {
            mensajes["IdGrupo"].Add("Se requiere un grupo de usuarios");
            hayError = true;
        }

        if ((op == Operacion.Creacion && dto.Activo == null))
        {
            mensajes["Activo"].Add("Se requiere un valor para el campo Activo");
            hayError = true;
        }

        // FALTA: Verificar que el nombre completo del usuario sea unico

        ResultadoValidacion v = new ResultadoValidacion();
        v.ValidacionOk = !hayError;

        if (hayError)
        {
            v.Mensajes = mensajes;
            Ok = false;
        }
        else
        {
            Ok = true;
        }

        return v;
    }
}