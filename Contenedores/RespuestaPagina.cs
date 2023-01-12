namespace Contenedores;

public class RespuestaPagina<T> : Respuesta<T>
{
    public int PaginaNro { get; set; }
    public int TamañoPagina { get; set; }
    public Uri PrimeraPagina { get; set; }
    public Uri UltimaPagina { get; set; }
    public Uri ProximaPagina { get; set; }
    public Uri PaginaAnterior { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }

    public RespuestaPagina(T datos, int paginaNro, int tamañoPagina)
    {
        this.PaginaNro = paginaNro;
        this.TamañoPagina = tamañoPagina;
        this.Datos = datos;
        Mensaje = string.Empty;
        DatosOk = true;
        Errores = null;
    }
}