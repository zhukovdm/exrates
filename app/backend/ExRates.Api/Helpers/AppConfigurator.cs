using System;
using System.IO;
using System.Linq;
using Asp.Versioning;
using ExRates.Application;
using ExRates.Domain;
using ExRates.Infrastructure;
using ExRates.Infrastructure.Cnb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ExRates.Api;

public static class AppConfigurator
{
    private static readonly string CorsPolicy = "ExRatesCors";

    public static void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }

    public static WebApplicationBuilder Configure(WebApplicationBuilder builder)
    {
        var phase = "Application Builder";
        Log.Information(phase);

        Log.Information("{Phase}: Application Settings", phase);
        builder.Services.Configure<CnbOptions>(builder.Configuration.GetSection(CnbOptions.Section));

        Log.Information("{Phase}: Cnb Options", phase);
        builder.Services.AddOptions<CnbOptions>()
            .Bind(builder.Configuration.GetSection(CnbOptions.Section))
            .ValidateDataAnnotations()
            .Validate(o =>
            {
                return Uri.TryCreate(o.OpenApi.BaseUrl, UriKind.Absolute, out var uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
            })
            .ValidateOnStart();

        Log.Information("{Phase}: ExRates Options", phase);
        builder.Services.AddOptions<ExRatesOptions>()
            .Bind(builder.Configuration.GetSection(ExRatesOptions.Section))
            .ValidateDataAnnotations()
            .Validate(o =>
            {
                return o.SourceCurrencies.Aggregate(
                    true, (acc, item) => acc && Currency.Create(item).NonEmpty);
            })
            .ValidateOnStart();

        Log.Information("{Phase}: Dependency Injection", phase);
        builder.Services
            .AddSingleton<IJsonParser, JsonParser>()
            .AddTransient<IExchangeRatesService, ExchangeRatesService>()
            .AddTransient<IExchangeRateProvider, CnbOpenApiExchangeRateProvider>();

        Log.Information("{Phase}: Create CORS Policy", phase);
        builder.Services.AddCors(cors => cors.AddPolicy(CorsPolicy, builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));

        Log.Information("{Phase}: Http Clients", phase);
        builder.Services
            .AddHttpClient<IHttpConnector, HttpConnector>();

        Log.Information("{Phase}: Serilog Logger", phase);
        builder.Host.UseSerilog();

        Log.Information("{Phase}: Controllers", phase);
        builder.Services.AddControllers();

        Log.Information("{Phase}: Endpoints API Explorer", phase);
        builder.Services.AddEndpointsApiExplorer();

        Log.Information("{Phase}: Generate Swagger UI", phase);
        builder.Services.AddSwaggerGen((g) =>
        {
            g.SwaggerDoc("v1", new OpenApiInfo { Title = "ExRates API - V1", Version = "1.0.0" });

            Directory
                .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList()
                .ForEach(f => g.IncludeXmlComments(f));
        });

        Log.Information("{Phase}: Health Checks", phase);
        builder.Services.AddHealthChecks();

        return builder;
    }

    public static WebApplication Configure(WebApplication app)
    {
        var phase = "Application Instance";
        Log.Information(phase);

        Log.Information("{Phase}: Swagger User Interface", phase);
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger().UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("v1/swagger.yaml", "ExRates API - V1");
            });
        }

        Log.Information("{Phase}: Use CORS Policy", phase);
        app.UseCors(CorsPolicy);

        Log.Information("{Phase}: Map Controllers", phase);
        app.MapControllers();

        Log.Information("{Phase}: Map Health Checks", phase);
        app.MapHealthChecks("/healthcheck");

        return app;
    }
}
