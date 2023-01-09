namespace Filtros;

public class FiltroPaginacion
{
    public int PaginaNro { get; set; }
    public int TamañoPagina { get; set; }
    public FiltroPaginacion()
    {
        this.PaginaNro = 1;
        this.TamañoPagina = 10;
    }
    public FiltroPaginacion(int paginaNro, int tamañoPagina)
    {
        this.PaginaNro = paginaNro < 1 ? 1 : paginaNro;
        this.TamañoPagina = tamañoPagina > 10 ? 10 : tamañoPagina;
    }
}