namespace DTOs;

public class RastreoPublicacionDTO
{
	public long? Id { get; set; } = null;
	public Guid? IdPublicacion { get; set; } = null;
	public int? IdFasePublicacion { get; set; } = null;
	public DateTime? Fecha { get; set; } = null;
	public Guid? IdUsuario { get; set; } = null;
	public long? TiempoEstimado { get; set; } = null;
	public int? IdFaseAnterior { get; set; } = null;
	public int? IdFaseSiguiente { get; set; } = null;
	public string? Comentarios { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
