using Contenedores;
using Filtros;
using Services;

public class Paginador
{
    public static RespuestaPagina<List<T>> CrearPaginaRespuesta<T>(List<T> data,
        FiltroPaginacion filtro, int totalRegistros, IUriService uriService, string ruta)
    {

        var respuesta = new RespuestaPagina<List<T>>(data, filtro.PaginaNro, filtro.TamañoPagina);
        var TotalPaginas = ((double)totalRegistros / (double)filtro.TamañoPagina);
        int numeroPaginas = Convert.ToInt32(Math.Ceiling(TotalPaginas));
        respuesta.ProximaPagina =
            filtro.PaginaNro >= 1 && filtro.PaginaNro < numeroPaginas
            ? uriService.GetPageUri(new FiltroPaginacion(filtro.PaginaNro + 1, filtro.TamañoPagina), ruta)
            : null;
        respuesta.PaginaAnterior =
            filtro.PaginaNro - 1 >= 1 && filtro.PaginaNro <= numeroPaginas
            ? uriService.GetPageUri(new FiltroPaginacion(filtro.PaginaNro - 1, filtro.TamañoPagina), ruta)
            : null;
        respuesta.PrimeraPagina = uriService.GetPageUri(new FiltroPaginacion(1, filtro.TamañoPagina), ruta);
        respuesta.UltimaPagina = uriService.GetPageUri(new FiltroPaginacion(numeroPaginas, filtro.TamañoPagina), ruta);
        respuesta.TotalPaginas = numeroPaginas;
        respuesta.TotalRegistros = totalRegistros;
        return respuesta;
    }
}