using Infra.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DependencyInjection
{
    public class ConfigureContexts
    {
        public static void Configure(IServiceCollection services, string bookConnectionString)
        {
            services.AddTransient(ctx => new Context(@bookConnectionString));
        }
    }
}
