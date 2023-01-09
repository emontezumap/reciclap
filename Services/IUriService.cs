using Filtros;

namespace Services;

public interface IUriService
{
    public Uri GetPageUri(FiltroPaginacion filtro, string ruta);
}