using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Herramientas;

public class Excepcionador
{
    Exception? ex;

    public Excepcionador(Exception ex)
    {
        this.ex = ex;
    }


    public GraphQLException ProcesarExcepcionActualizacionDB()
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
        {
            string dup2 = @"duplicate key value is (\(.+\))";
            Match match = Regex.Match(mensaje, dup2, RegexOptions.IgnoreCase);
            string valor = match.Groups[1].Value;

            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Registro duplicado: " + valor)
               .SetCode(CodigosError.ERR_REGISTRO_DUPLICADO.ToString());
        }
        else if (listaReferencia.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
        {
            string inex = @"conflicted with the FOREIGN KEY constraint .+(FK_.+)\. ";
            Match match = Regex.Match(mensaje, inex, RegexOptions.IgnoreCase);
            string fk = match.Groups[1].Value.Replace("\\", "").Replace("\"", "");
            string valor = fk.Substring(fk.LastIndexOf("_id_") + 1);

            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Id de objeto inexistente: " + valor)
               .SetCode(CodigosError.ERR_ID_INEXISTENTE.ToString());
        }
        else
        {
            error = (ErrorBuilder)ErrorBuilder.New()
               .SetMessage("Error no especificado")
               .SetMessage("Mensaje original:" + mensaje)
               .SetCode(CodigosError.ERR_ERROR_NO_ESPECIFICADO.ToString());
        }

        return new GraphQLException(error.Build());
    }

    private GraphQLException ExcepcionRegistroNoExiste()
    {
        var error = ErrorBuilder.New()
            .SetMessage("Registro eliminado")
            .SetCode(CodigosError.ERR_REGISTRO_ELIMINADO.ToString())
            .Build();

        return new GraphQLException(error);
    }

    public GraphQLException ExcepcionAutenticacion()
    {
        var error = ErrorBuilder.New()
            .SetMessage("Usuario no autenticado")
            .SetCode(CodigosError.ERR_NO_AUTENTICADO.ToString())
            .Build();

        return new GraphQLException(error);
    }
}
