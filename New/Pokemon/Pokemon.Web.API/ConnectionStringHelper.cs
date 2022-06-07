namespace Pokemon.Web.API;

public static class ConnectionStringHelper
{
    public static string AddPortToConnectionString(this string str)
    {
        var port = Environment.GetEnvironmentVariable("POKEMON_PORTer") ?? "1433";
        return str.Replace("POKEMON_PORT", port);
    }
}