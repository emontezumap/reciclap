
using DB;
using DTOs;
using Herramientas;
using Services;

namespace Validadores;

public class ValidadorUsuario : IValidadorEntidad
{
    private Dictionary<string, HashSet<string>> mensajes;
    private UsuarioDTO dto;
    private Operacion op;
    private SSDBContext ctx;
    private bool ValidarReferencias;

    public bool Ok { get; set; } = false;

    public ValidadorUsuario(UsuarioDTO dto, Operacion op, SSDBContext ctx, bool ValidarReferencias = false)
    {
        mensajes = new Dictionary<string, HashSet<string>>() {
			{ "Id", new HashSet<string>() },
			{ "Nombre", new HashSet<string>() },
			{ "Apellido", new HashSet<string>() },
			{ "SegundoNombre", new HashSet<string>() },
			{ "SegundoApellido", new HashSet<string>() },
			{ "Perfil", new HashSet<string>() },
			{ "Direccion", new HashSet<string>() },
			{ "IdCiudad", new HashSet<string>() },
			{ "Telefono", new HashSet<string>() },
			{ "Telefono2", new HashSet<string>() },
			{ "Email", new HashSet<string>() },
			{ "Clave", new HashSet<string>() },
			{ "Email2", new HashSet<string>() },
			{ "IdProfesion", new HashSet<string>() },
			{ "MaximoPublicaciones", new HashSet<string>() },
			{ "IdGrupo", new HashSet<string>() },
			{ "Estatus", new HashSet<string>() },
			{ "IdTipoUsuario", new HashSet<string>() },
			{ "IdRol", new HashSet<string>() },
			{ "UltimaIp", new HashSet<string>() },
			{ "Activo", new HashSet<string>() }
        };

        this.dto = dto;
        this.op = op;
        this.ctx = ctx;
        this.ValidarReferencias = ValidarReferencias;
    }

    public async Task<ResultadoValidacion> Validar()
    {

        if (op == Operacion.Modificacion)
        {
            if (dto.Id == null)
                mensajes["Id"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (await ctx.Usuarios.FindAsync(dto.Id) == null)
                mensajes["Id"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }

        if (string.IsNullOrEmpty(dto.Nombre))
        {
            if (op == Operacion.Creacion)
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Nombre != null)   // Cadena vacia
                mensajes["Nombre"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Nombre != null && dto.Nombre.Length > 100)
            mensajes["Nombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Apellido))
        {
            if (op == Operacion.Creacion)
                mensajes["Apellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Apellido != null)   // Cadena vacia
                mensajes["Apellido"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Apellido != null && dto.Apellido.Length > 100)
            mensajes["Apellido"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.SegundoNombre != null && dto.SegundoNombre.Length > 50)
            mensajes["SegundoNombre"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.SegundoApellido != null && dto.SegundoApellido.Length > 50)
            mensajes["SegundoApellido"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.Perfil != null && dto.Perfil.Length > 200)
            mensajes["Perfil"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Direccion))
        {
            if (op == Operacion.Creacion)
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Direccion != null)   // Cadena vacia
                mensajes["Direccion"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Direccion != null && dto.Direccion.Length > 300)
            mensajes["Direccion"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdCiudad == null)
            mensajes["IdCiudad"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdCiudad != null && await ctx.Varias.FindAsync(dto.IdCiudad) == null)
            mensajes["IdCiudad"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (string.IsNullOrEmpty(dto.Telefono))
        {
            if (op == Operacion.Creacion)
                mensajes["Telefono"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Telefono != null)   // Cadena vacia
                mensajes["Telefono"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Telefono != null && dto.Telefono.Length > 20)
            mensajes["Telefono"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.Telefono2 != null && dto.Telefono2.Length > 20)
            mensajes["Telefono2"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Email))
        {
            if (op == Operacion.Creacion)
                mensajes["Email"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Email != null)   // Cadena vacia
                mensajes["Email"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Email != null && dto.Email.Length > 300)
            mensajes["Email"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (string.IsNullOrEmpty(dto.Clave))
        {
            if (op == Operacion.Creacion)
                mensajes["Clave"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
            else if (dto.Clave != null)   // Cadena vacia
                mensajes["Clave"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());
        }

        if (dto.Clave != null && dto.Clave.Length > 256)
            mensajes["Clave"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (dto.Email2 != null && dto.Email2.Length > 250)
            mensajes["Email2"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (ValidarReferencias && dto.IdProfesion != null && await ctx.Varias.FindAsync(dto.IdProfesion) == null)
            mensajes["IdProfesion"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (op == Operacion.Creacion && dto.IdGrupo == null)
            mensajes["IdGrupo"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdGrupo != null && await ctx.Varias.FindAsync(dto.IdGrupo) == null)
            mensajes["IdGrupo"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (dto.Estatus != null && dto.Estatus.Length > 2)
            mensajes["Estatus"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        if (op == Operacion.Creacion && dto.IdTipoUsuario == null)
            mensajes["IdTipoUsuario"].Add(CodigosError.ERR_CAMPO_REQUERIDO.ToString());

        if (ValidarReferencias && dto.IdTipoUsuario != null && await ctx.Varias.FindAsync(dto.IdTipoUsuario) == null)
            mensajes["IdTipoUsuario"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (ValidarReferencias && dto.IdRol != null && await ctx.Varias.FindAsync(dto.IdRol) == null)
            mensajes["IdRol"].Add(CodigosError.ERR_ID_INEXISTENTE.ToString());

        if (dto.UltimaIp != null && dto.UltimaIp.Length > 20)
            mensajes["UltimaIp"].Add(CodigosError.ERR_CADENA_MUY_LARGA.ToString());        

        bool hayError = false;

        foreach (KeyValuePair<string, HashSet<string>> entry in mensajes)
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
