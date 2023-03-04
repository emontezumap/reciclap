
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorUsuario : IValidadorEntidad
{
    private Dictionary<string, HashSet<CodigosError>> mensajes;
    private UsuarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool SinReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorUsuario(UsuarioDTO dto, Operacion op, SSDBContext ctx, bool SinReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<CodigosError>>() {
			{ "Id", new HashSet<CodigosError>() },
			{ "Nombre", new HashSet<CodigosError>() },
			{ "Apellido", new HashSet<CodigosError>() },
			{ "SegundoNombre", new HashSet<CodigosError>() },
			{ "SegundoApellido", new HashSet<CodigosError>() },
			{ "Perfil", new HashSet<CodigosError>() },
			{ "Direccion", new HashSet<CodigosError>() },
			{ "IdCiudad", new HashSet<CodigosError>() },
			{ "Telefono", new HashSet<CodigosError>() },
			{ "Telefono2", new HashSet<CodigosError>() },
			{ "Email", new HashSet<CodigosError>() },
			{ "Clave", new HashSet<CodigosError>() },
			{ "Email2", new HashSet<CodigosError>() },
			{ "IdProfesion", new HashSet<CodigosError>() },
			{ "MaximoPublicaciones", new HashSet<CodigosError>() },
			{ "IdGrupo", new HashSet<CodigosError>() },
			{ "Estatus", new HashSet<CodigosError>() },
			{ "IdTipoUsuario", new HashSet<CodigosError>() },
			{ "IdRol", new HashSet<CodigosError>() },
			{ "UltimaIp", new HashSet<CodigosError>() },
			{ "Activo", new HashSet<CodigosError>() }
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
        this.SinReferencias = SinReferencias;
    }

    public async Task<ResultadoValidacion> Validar()
    {

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
                mensajes["Id"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (await ctx.Usuarios.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE);
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Nombre != null && dto.Nombre.Length > 100)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Apellido))
        {
            if (op == Operacion.Creacion)
                mensajes["Apellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Apellido != null)   // Cadena vacia
                mensajes["Apellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Apellido != null && dto.Apellido.Length > 100)
            mensajes["Apellido"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.SegundoNombre))
        {
            if (op == Operacion.Creacion)
                mensajes["SegundoNombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.SegundoNombre != null)   // Cadena vacia
                mensajes["SegundoNombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.SegundoNombre != null && dto.SegundoNombre.Length > 50)
            mensajes["SegundoNombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.SegundoApellido))
        {
            if (op == Operacion.Creacion)
                mensajes["SegundoApellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.SegundoApellido != null)   // Cadena vacia
                mensajes["SegundoApellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.SegundoApellido != null && dto.SegundoApellido.Length > 50)
            mensajes["SegundoApellido"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Perfil))
        {
            if (op == Operacion.Creacion)
                mensajes["Perfil"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Perfil != null)   // Cadena vacia
                mensajes["Perfil"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Perfil != null && dto.Perfil.Length > 200)
            mensajes["Perfil"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Direccion))
        {
            if (op == Operacion.Creacion)
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Direccion != null)   // Cadena vacia
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Direccion != null && dto.Direccion.Length > 300)
            mensajes["Direccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (!SinReferencias && dto.IdCiudad != null && await ctx.Varias.FindAsync(dto.IdCiudad) == null)
            mensajes["IdCiudad"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Telefono))
        {
            if (op == Operacion.Creacion)
                mensajes["Telefono"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Telefono != null)   // Cadena vacia
                mensajes["Telefono"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Telefono != null && dto.Telefono.Length > 20)
            mensajes["Telefono"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Telefono2))
        {
            if (op == Operacion.Creacion)
                mensajes["Telefono2"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Telefono2 != null)   // Cadena vacia
                mensajes["Telefono2"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Telefono2 != null && dto.Telefono2.Length > 20)
            mensajes["Telefono2"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Email))
        {
            if (op == Operacion.Creacion)
                mensajes["Email"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Email != null)   // Cadena vacia
                mensajes["Email"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Email != null && dto.Email.Length > 300)
            mensajes["Email"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Clave))
        {
            if (op == Operacion.Creacion)
                mensajes["Clave"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Clave != null)   // Cadena vacia
                mensajes["Clave"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Clave != null && dto.Clave.Length > 256)
            mensajes["Clave"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (string.IsNullOrEmpty(dto.Email2))
        {
            if (op == Operacion.Creacion)
                mensajes["Email2"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Email2 != null)   // Cadena vacia
                mensajes["Email2"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Email2 != null && dto.Email2.Length > 250)
            mensajes["Email2"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (!SinReferencias && dto.IdProfesion != null && await ctx.Varias.FindAsync(dto.IdProfesion) == null)
            mensajes["IdProfesion"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdGrupo != null && await ctx.Varias.FindAsync(dto.IdGrupo) == null)
            mensajes["IdGrupo"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.Estatus))
        {
            if (op == Operacion.Creacion)
                mensajes["Estatus"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.Estatus != null)   // Cadena vacia
                mensajes["Estatus"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.Estatus != null && dto.Estatus.Length > 2)
            mensajes["Estatus"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        if (op == Operacion.Creacion && dto.IdTipoUsuario == null)
            mensajes["IdTipoUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO);

        if (!SinReferencias && dto.IdTipoUsuario != null && await ctx.Varias.FindAsync(dto.IdTipoUsuario) == null)
            mensajes["IdTipoUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (!SinReferencias && dto.IdRol != null && await ctx.Varias.FindAsync(dto.IdRol) == null)
            mensajes["IdRol"].Add(CodigosError.ERR_ID_INEXISTENTE);

        if (string.IsNullOrEmpty(dto.UltimaIp))
        {
            if (op == Operacion.Creacion)
                mensajes["UltimaIp"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
            else if (dto.UltimaIp != null)   // Cadena vacia
                mensajes["UltimaIp"].Add(CodigosError.ERR_CAMPO_REQUERIDO);
        }

        if (dto.UltimaIp != null && dto.UltimaIp.Length > 20)
            mensajes["UltimaIp"].Add(CodigosError.ERR_CADENA_MUY_LARGA);        

        bool hayError = false;

        foreach (KeyValuePair<string, HashSet<CodigosError>> entry in mensajes)
        {
            if (entry.Value.Count > 0)
            {
                hayError = true;
                break;
            }
        }

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
