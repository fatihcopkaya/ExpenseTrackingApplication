using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasrafTakipYonetim.Infrastructure
{
   public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));

        }
    }
}
