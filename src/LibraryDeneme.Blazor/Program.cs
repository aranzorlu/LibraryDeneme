using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Serilog.Events;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Volo.Abp.Modularity;


namespace LibraryDeneme.Blazor;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        SyncfusionLicenseProvider.RegisterLicense("MzQ0MTI1M0AzMjM2MmUzMDJlMzBtamZSdlVseUw2SGhSakVRZWgxeVhQZENuNHR3SVpxdWZTTEREZ1o3NmZVPQ==");
        try
        {
            
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAuthenticatedUser", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy("AllowAnonymous", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.Resource is RouteEndpoint endpoint &&
                        endpoint.RoutePattern.RawText.Equals("/xbooks", StringComparison.OrdinalIgnoreCase)
                    );
                });
            });

            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    loggerConfiguration
#if DEBUG
                        .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.Async(c => c.File("Logs/logs.txt"))
                        .WriteTo.Async(c => c.Console())
                        .WriteTo.Async(c => c.AbpStudio(services));
                });
            await builder.AddApplicationAsync<LibraryDenemeBlazorModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
       
    }
   

        
    
}
