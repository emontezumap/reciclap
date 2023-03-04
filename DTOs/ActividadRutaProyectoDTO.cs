namespace DTOs;

public class ActividadRutaProyectoDTO
{
	public int? Id { get; set; } = null;
	public int? IdRutaProyecto { get; set; } = null;
	public string? Descripcion { get; set; } = null;
	public int? Secuencia { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
