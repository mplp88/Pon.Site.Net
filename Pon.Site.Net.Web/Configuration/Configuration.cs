using Pon.Site.Net.Web.Services;

namespace Pon.Site.Net.Web.Configuration
{
    public static class Configuration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<WeatherForecastService>();
            services.AddScoped<IToDoService, ToDoService>();
        }
    }
}
