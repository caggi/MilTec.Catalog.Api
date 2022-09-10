using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace Application.Core
{
    /// <summary>
    /// Configura a visualização da documentação da API
    /// </summary>
    internal static class ConfigureSwagger
    {
        internal static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            builder.Services.AddSwaggerGenNewtonsoftSupport();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Desafio-Mil Tec & EmpowerMind - API Catálogo de livros",
                    Description = "Uma ASP.NET Core Web API para buscar livros em um catálogo de um arquivo JSON.",
                    Contact = new OpenApiContact
                    {
                        Name = "LinkedIn",
                        Url = new Uri("https://www.linkedin.com/in/caggi/")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
