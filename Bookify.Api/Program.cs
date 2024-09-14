using Asp.Versioning;
using Bookify.Api.Extensions;
using Bookify.Api.Modules.Apartments;
using Bookify.Api.Modules.Bookings;
using Bookify.Api.Modules.Users;
using Bookify.Api.OpenApi;
using Bookify.Application;
using Bookify.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace Bookify.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddApplicationLayer();

        builder.Services.AddInfrastructureLayer(builder.Configuration);

        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                app
                    .DescribeApiVersions()
                    .Select(description => new
                    {
                        Url = $"/swagger/{description.GroupName}/swagger.json",
                        Name = description.GroupName.ToUpperInvariant()
                    })
                    .ToList()
                    .ForEach(endpoint => options.SwaggerEndpoint(endpoint.Url, endpoint.Name));
            });

            app.ApplyMigrations();

            app.SeedData();
        }

        app.UseHttpsRedirection();

        app.USeRequestContextLogging();

        app.UseSerilogRequestLogging();

        app.UseCustomExceptionHandler();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        var apiVersionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var routeGroupBuilder = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        routeGroupBuilder.MapBookingEndpoints();
        routeGroupBuilder.MapApartmentsEndpoints();
        routeGroupBuilder.MapUsersEndpoints();

        app.MapHealthChecks("health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.Run();
    }
}
