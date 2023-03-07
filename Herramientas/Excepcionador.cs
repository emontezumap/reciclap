using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Herramientas;

public class Excepcionador
{
    private readonly Dictionary<string, Dictionary<string, HashSet<string>>>? rvs;
    private const string MENSAJE_ERROR_POR_DEFECTO = "Error no especificado";
    private const string MENSAJE_ERROR_REGISTRO_ELIMINADO = "El registro fue eliminado";
    private const string MENSAJE_ERROR_DATOS_NO_VALIDOS = "Datos no válidos";
    private const string MENSAJE_ERROR_REGISTRO_NO_EXISTE = "El registro especificado no existe";
    public Excepcionador() { }

    public Excepcionador(Dictionary<string, Dictionary<string, HashSet<string>>> rvs)
    {
        this.rvs = rvs;
    }

    public GraphQLException ProcesarExcepcionActualizacionDB(Exception? ex)
    {
        string mensaje;

        if (ex == null)
            mensaje = "";
        else if (ex.GetType() == typeof(DbUpdateConcurrencyException))
            return ExcepcionRegistroNoExiste();
        else
        {
            ex = ex.InnerException != null ? ex.InnerException : ex;
            mensaje = ex.Message != null ? ex.Message : "";
        }

        ErrorBuilder error;
        List<string> listaDuplicados = new List<string>() { "duplicate key" };
        List<string> listaReferencia = new List<string>() { "conflicted", "FOREIGN KEY constraint" };

        if (listaDuplicados.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Registro duplicado")
               .SetCode(CodigosError.ERR_REGISTRO_DUPLICADO.ToString());
        else if (listaReferencia.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Id de objeto inexistente")
               .SetCode(CodigosError.ERR_ID_INEXISTENTE.ToString());
        else
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Error no especificado")
               .SetMessage("Mensaje original:" + mensaje)
               .SetCode(CodigosError.ERR_ERROR_NO_ESPECIFICADO.ToString());

        return new GraphQLException(error.Build());
    }

    public GraphQLException ExcepcionRegistroNoExiste()
    {
        var error = ErrorBuilder.New()
            .SetMessage("Registro eliminado")
            .SetCode(CodigosError.ERR_REGISTRO_ELIMINADO.ToString())
            .Build();

        return new GraphQLException(error);
    }

    public GraphQLException ExcepcionDatosNoValidos()
    {
        var error = ErrorBuilder.New();
        error.SetMessage("Datos no válidos");
        error.SetCode(CodigosError.ERR_DATOS_NO_VALIDOS.ToString());

        if (rvs != null)
        {
            foreach (KeyValuePair<string, Dictionary<string, HashSet<string>>> obj in rvs)
                error.SetExtension(obj.Key, JsonConvert.SerializeObject(obj.Value));
        }
        return new GraphQLException(error.Build());
    }
}
