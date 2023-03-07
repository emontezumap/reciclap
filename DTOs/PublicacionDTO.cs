namespace DTOs;

public class PublicacionDTO
{
	public Guid? Id { get; set; } = null;
	public string? Titulo { get; set; } = null;
	public string? Descripcion { get; set; } = null;
	public DateTime? Fecha { get; set; } = null;
	public long? Consecutivo { get; set; } = null;
	public Guid? IdPublicador { get; set; } = null;
	public int? Gustan { get; set; } = null;
	public int? NoGustan { get; set; } = null;
	public int? IdEstatusPublicacion { get; set; } = null;
	public int? IdFasePublicacion { get; set; } = null;
	public int? IdTipoPublicacion { get; set; } = null;
	public int? IdClasePublicacion { get; set; } = null;
	public Guid? IdRevisadaPor { get; set; } = null;
	public Guid? IdImagenPrincipal { get; set; } = null;
	public int? TiempoEstimado { get; set; } = null;
	public int? Posicionamiento { get; set; } = null;
	public int? Secuencia { get; set; } = null;
	public int? Vistas { get; set; } = null;
	public decimal? Evaluacion { get; set; } = null;
	public string? DireccionIpCreacion { get; set; } = null;
	public string? Dispositivo { get; set; } = null;
	public string? Direccion { get; set; } = null;
	public string? ReferenciasDireccion { get; set; } = null;
	public DateTime? FechaDisponible { get; set; } = null;
	public decimal? TotalArticulos { get; set; } = null;
	public Guid? IdProyecto { get; set; } = null;
	public decimal? CostoEstimado { get; set; } = null;
	public string? IdMonedaCostoEstimado { get; set; } = null;
	public decimal? TipoCambioCostoEstimado { get; set; } = null;
	public decimal? CostoReal { get; set; } = null;
	public decimal? MontoInversion { get; set; } = null;
	public decimal? CostoRealTraslado { get; set; } = null;
	public string? IdMonedaCostoReal { get; set; } = null;
	public decimal? TipoCambioCostoReal { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
