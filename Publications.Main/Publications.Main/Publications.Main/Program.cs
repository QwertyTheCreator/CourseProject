using GraphQL.AspNet.Configuration;
using GraphQL.AspNet.Security;
using GraphQL.AspNet.ServerExtensions.MultipartRequests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Publications.Main.Application;
using Publications.Main.Application.Utils;
using Publications.Main.Infrastructure;
using Publications.Main.WebAPI.Configuration;

namespace Publications.Main;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.WebHost.UseUrls("https://192.168.0.121:7056");
        builder.Services.AddGraphQL(o =>
        {
            o.QueryHandler.Route = "/api/graphql";
            o.ResponseOptions.ExposeExceptions = true;
            o.AuthorizationOptions.Method = AuthorizationMethod.PerRequest;
            o.AddMultipartRequestSupport();          
        });
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });

        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddCors();

        builder.Services.AddInfrastructureServices();
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder =>
            builder
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());

        app.UseHttpsRedirection();
        app.UseGraphQL();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.ApplyDatabaseMigrations();

        app.Run();
    }
}
