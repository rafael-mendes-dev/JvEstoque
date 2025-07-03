namespace JvEstoque.Web.Common;

public static class AdicionaEspaçoAntesMaiuscula
{
    public static string AdicionarEspacoAntesMaiuscula(this string texto)
    {
        return System.Text.RegularExpressions.Regex.Replace(texto, "(?<!^)([A-Z])", " $1");
    }
}