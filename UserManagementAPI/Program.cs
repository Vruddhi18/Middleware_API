using Microsoft.OpenApi.Models;
using UserManagementAPI.CustomMiddlewares;

namespace UserManagementAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        ConfigureSwagger(builder);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ErrorHandler>();
        app.UseMiddleware<AuthenticationMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseAuthorization();
        app.UseAuthentication();
        app.MapControllers();

        await app.RunAsync();
    }

    /* This method configures Swagger for the API documentation.
     Refactored to be a separate method for better organization and readability. */
    // Do not use this method in production code. Since this is a sample code, we are using it for demonstration purposes.
    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagementAPI", Version = "v1" });

            // Add JWT Authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}