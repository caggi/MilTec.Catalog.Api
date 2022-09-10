using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DependencyInjection
{
    public static class ConfigureRepositories
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<CatalogRepository>();
        }
    }
}