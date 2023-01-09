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
        var _endpointUri = new Uri(string.Concat(baseUri, ruta));
        var modifiedUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "paginaNro", filtro.PaginaNro.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "tamañoPagina", filtro.TamañoPagina.ToString());
        return new Uri(modifiedUri);
    }
}