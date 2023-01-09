using Microsoft.AspNetCore.WebUtilities;
using Filtros;

namespace Services;

public class UriService : IUriService
{
    private readonly string baseUri;
    public UriService(string baseUri)
    {
        this.baseUri = baseUri;
    }

    public Uri GetPageUri(FiltroPaginacion filtro, string ruta)
    {
        var _enpointUri = new Uri(string.Concat(baseUri, ruta));
        var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filtro.PaginaNro.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filtro.TamañoPagina.ToString());
        return new Uri(modifiedUri);
    }
}