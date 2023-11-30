namespace GLPIBot.Api.Web.Helper
{
    public static class ConversorHelper
    {
        public static int converterParaInteiro(string numeroPraConverter)
        {
            int numero = 0;
            Int32.TryParse(numeroPraConverter, out numero);
            return numero;
        }
    }
}
