using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Herramientas;

public class Excepcionador
{
    private readonly ResultadoValidacion? rv;
    private readonly ICollection<ResultadoValidacion>? rvs;
    private const string MENSAJE_ERROR_POR_DEFECTO = "Error no especificado";
    private const string MENSAJE_ERROR_REGISTRO_ELIMINADO = "El registro fue eliminado";
    private const string MENSAJE_ERROR_DATOS_NO_VALIDOS = "Datos no válidos";
    private const string MENSAJE_ERROR_REGISTRO_NO_EXISTE = "El registro especificado no existe";
    public Excepcionador() { }

    public Excepcionador(ResultadoValidacion rv)
    {
        this.rv = rv;
    }

    public Excepcionador(ICollection<ResultadoValidacion> rvs)
    {
        this.rvs = rvs;
    }

    public GraphQLException ProcesarExcepcionActualizacionDB(Exception? ex)
    {
        return ProcesarExcepcionActualizacionDB(ex, "");
    }

    public GraphQLException ProcesarExcepcionActualizacionDB(Exception? ex, string objeto)
    {
        string mensaje, esp, est;

        if (ex == null)
        {
            mensaje = "";
            esp = "";
            est = "";
        }
        else if (ex.GetType() == typeof(DbUpdateConcurrencyException))
        {
            return ExcepcionRegistroNoExiste();
        }
        else
        {
            ex = ex.InnerException != null ? ex.InnerException : ex;
            mensaje = ex.Message != null ? ex.Message : "";
            esp = objeto.StartsWith("la", StringComparison.CurrentCultureIgnoreCase) ? "especificada" : "especificado";
            est = objeto.StartsWith("la", StringComparison.CurrentCultureIgnoreCase) ? "ésta" : "éste";
        }

        ErrorBuilder error;
        List<string> listaDuplicados = new List<string>() { "insert", "duplicate key" };
        List<string> listaReferencia = new List<string>() { "delete", "reference" };

        if (listaDuplicados.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage($"{objeto} {esp} ya existe en la base de datos")
               .SetCode("REGISTRO_DUPLICADO");
        else if (listaReferencia.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage($"No se puede eliminar {objeto} {esp}; existen registros que dependen de {est}")
               .SetCode("ELIMINACION_NO_PERMITIDA");
        else
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage(MENSAJE_ERROR_POR_DEFECTO)
               .SetMessage("Mensaje original:" + mensaje)
               .SetCode("ERROR_ACTUALIZACION_NO_ESPECIFICADO");

        return new GraphQLException(error.Build());
    }

    public GraphQLException ExcepcionRegistroEliminado()
    {
        var error = ErrorBuilder.New()
            .SetMessage(MENSAJE_ERROR_REGISTRO_ELIMINADO)
            .SetCode("REGISTRO_ELIMINADO")
            .Build();

        return new GraphQLException(error);
    }

    public GraphQLException ExcepcionRegistroNoExiste()
    {
        var error = ErrorBuilder.New()
            .SetMessage(MENSAJE_ERROR_REGISTRO_NO_EXISTE)
            .SetCode("REGISTRO_INEXISTENTE")
            .Build();

        return new GraphQLException(error);
    }

    public GraphQLException ExcepcionDatosNoValidos()
    {
        var error = ErrorBuilder.New();
        error.SetMessage(MENSAJE_ERROR_DATOS_NO_VALIDOS);
        error.SetCode("DATOS_NO_VALIDOS");

        if (rv != null && rv.Mensajes != null)
        {
            foreach (var men in rv.Mensajes)
                if (men.Value.Count > 0)
                    error.SetExtension(men.Key, JsonConvert.SerializeObject(men.Value));
        }
        else if (rvs != null)
        {
            foreach (var rv in rvs)
            {
                if (rv.Mensajes != null)
                {
                    foreach (var men in rv.Mensajes)
                    {

                    }
                }
            }
        }
        return new GraphQLException(error.Build());
    }
}
