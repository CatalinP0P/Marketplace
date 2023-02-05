using Microsoft.AspNetCore.Mvc;

using Marketplace.Interfaces;
using Marketplace.Models;

namespace Marketplace.Controllers.Api
{
    [ApiController]
    [Route("api/{controller}")]
    public class LocationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IRoLocationService _locationService;

        public LocationController(HttpClient httpClient, IRoLocationService locationService)
        {
            _httpClient = httpClient;
            _locationService = locationService;
        }

        [HttpGet]
        [Route("counties")]
        public async Task<List<County>> GetCounties()
        {
            return await _locationService.GetCounties();
        }

        [HttpGet]
        [Route("cities")]
        public async Task<List<City>> GetCities()
        {
            return await _locationService.GetCities();
        }

        [HttpGet]
        [Route("cities/{county}")]
        public async Task<List<City>> GetCitiesOfCounty(string county)
        {
            return await _locationService.GetCitiesInCounty(county);
        }
    }
}