namespace DTOs;

public class ProyectoDTO
{
	public Guid? Id { get; set; } = null;
	public string? Titulo { get; set; } = null;
	public string? Descripcion { get; set; } = null;
	public DateTime? FechaInicio { get; set; } = null;
	public Guid? IdGerente { get; set; } = null;
	public Guid? IdRevisor { get; set; } = null;
	public int? Gustan { get; set; } = null;
	public int? NoGustan { get; set; } = null;
	public int? IdEstatusPublicacion { get; set; } = null;
	public int? IdEstatusProyecto { get; set; } = null;
	public Guid? IdRevisadaPor { get; set; } = null;
	public Guid? IdImagenPrincipal { get; set; } = null;
	public int? IdTipoProyecto { get; set; } = null;
	public int? TiempoEstimado { get; set; } = null;
	public int? ProgresoEstimado { get; set; } = null;
	public int? ProgresoReal { get; set; } = null;
	public decimal? Evaluacion { get; set; } = null;
	public int? IdRutaProyecto { get; set; } = null;
	public int? IdFaseAnterior { get; set; } = null;
	public int? IdFaseSiguiente { get; set; } = null;
	public DateTime? FechaDisponible { get; set; } = null;
	public decimal? TotalArticulos { get; set; } = null;
	public decimal? CostoEstimado { get; set; } = null;
	public string? IdMonedaCostoEstimado { get; set; } = null;
	public decimal? TipoCambioCostoEstimado { get; set; } = null;
	public decimal? CostoReal { get; set; } = null;
	public string? IdMonedaCostoReal { get; set; } = null;
	public decimal? TipoCambioCostoReal { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
