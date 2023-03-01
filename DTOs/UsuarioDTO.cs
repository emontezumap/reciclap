namespace DTOs;

public class UsuarioDTO
{
    public Guid? Id { get; set; } = null;
    public string? Nombre { get; set; } = null;
    public string? Nombre2 { get; set; } = null;
    public string? Apellido { get; set; } = null;
    public string? Apellido2 { get; set; } = null;
    public string? Perfil { get; set; } = null;
    public string? Direccion { get; set; } = null;
    public int? IdCiudad { get; set; } = null;
    public string? Telefono { get; set; } = null;
    public string? Telefono2 { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Clave { get; set; } = null;
    public string? Email2 { get; set; } = null;
    public int? IdProfesion { get; set; } = null;
    public int? MaximoPublicaciones { get; set; } = null;
    public int? IdGrupo { get; set; } = null;
    public bool? Activo { get; set; } = null;
}
