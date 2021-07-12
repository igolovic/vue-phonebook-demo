using Microsoft.Extensions.DependencyInjection;

using phonebook_webapi.Logging;

namespace phonebook_webapi
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
