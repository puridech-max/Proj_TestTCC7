using Microsoft.Extensions.Configuration;
using ProductCodeApp.Api;

namespace ProductCodeApp;

static class Program
{
    internal static HttpClient ApiHttp { get; private set; } = null!;
    internal static ProductApiClient Api { get; private set; } = null!;


    [STAThread]
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var baseUrl = configuration["ApiBaseUrl"]?.Trim().TrimEnd('/');
        if (string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = "https://localhost:7111";
        }

        ApiHttp = new HttpClient
        {
            BaseAddress = new Uri(baseUrl + "/"),
            Timeout = TimeSpan.FromSeconds(30)
        };
        Api = new ProductApiClient(ApiHttp);

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
