using System.Text.RegularExpressions;
using Herramientas;
using Microsoft.EntityFrameworkCore;

public class DatosError
{
    private Exception ex;

    public string Codigo { get; set; } = "";
    public string Objeto { get; set; } = "";
    public string Valor { get; set; } = "";

    public DatosError(Exception ex)
    {
        this.ex = ex;

        ExtraerInfoError();
    }

    private void ExtraerInfoError()
    {
        string mensaje = "";

        if (ex.GetType() == typeof(DbUpdateConcurrencyException))
            Codigo = CodigosError.ERR_REGISTRO_ELIMINADO.ToString();
        else
        {
            ex = ex.InnerException != null ? ex.InnerException : ex;
            mensaje = ex.Message != null ? ex.Message : "";

            List<string> listaDuplicados = new List<string>() { "duplicate key" };
            List<string> listaReferencia = new List<string>() { "conflicted", "FOREIGN KEY constraint" };

            if (listaDuplicados.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
                Codigo = CodigosError.ERR_REGISTRO_DUPLICADO.ToString();
            else if (listaReferencia.All(p => mensaje.Contains(p, StringComparison.CurrentCultureIgnoreCase)))
                Codigo = CodigosError.ERR_ID_INEXISTENTE.ToString();
            else
                Codigo = CodigosError.ERR_ERROR_NO_ESPECIFICADO.ToString();
        }

        Match match;

        switch (Codigo)
        {
            case "ERR_REGISTRO_ELIMINADO":
                break;
            case "ERR_REGISTRO_DUPLICADO":
                string dup1 = @"duplicate key row in object 'dbo\.(\w+)'";
                string dup2 = @"duplicate key value is (\(.+\))";
                match = Regex.Match(mensaje, dup1, RegexOptions.IgnoreCase);
                Objeto = match.Groups[1].Value;
                match = Regex.Match(mensaje, dup2, RegexOptions.IgnoreCase);
                Valor = match.Groups[1].Value;
                break;
            case "ERR_ID_INEXISTENTE":
                string inex = @"conflicted with the FOREIGN KEY constraint .+(FK_.+)\. ";
                match = Regex.Match(mensaje, inex, RegexOptions.IgnoreCase);
                string fk = match.Groups[1].Value.Replace("\\", "").Replace("\"", "");
                Objeto = fk.Substring(fk.LastIndexOf("_id_") + 1);
                break;
        }
    }
}
