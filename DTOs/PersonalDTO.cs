namespace DTOs;

public class PersonalDTO
{
	public long? Id { get; set; } = null;
	public Guid? IdPublicacion { get; set; } = null;
	public Guid? IdUsuario { get; set; } = null;
	public DateTime? Fecha { get; set; } = null;
	public int? IdRol { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
