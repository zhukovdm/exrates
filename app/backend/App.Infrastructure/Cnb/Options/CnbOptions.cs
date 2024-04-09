namespace App.Infrastructure.Cnb;

public sealed class CnbOptions
{
    public static readonly string Section = "Cnb";

    public OpenApiOptions OpenApi { get; set; } = null!;

    public class OpenApiOptions
    {
        public string BaseUrl { get; set; } = null!;
    }
}
