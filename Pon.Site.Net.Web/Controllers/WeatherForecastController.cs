using Microsoft.AspNetCore.Mvc;
using Pon.Site.Net.Web.Services;

namespace Pon.Site.Net.Web.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly WeatherForecastService _service;

        public WeatherForecastController(WeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var forecasts = await _service.GetForecast();
            return View(forecasts);
        }
    }
}
