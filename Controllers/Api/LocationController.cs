using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Interfaces;

namespace Marketplace.Controllers.Api
{
    [ApiController]
    [Route("api/{controller}")]
    public class LocationController : Controller
    {
        private readonly IRoLocationService _locationService;
        public LocationController(IRoLocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [Route("counties")]
        public async Task<List<string>> GetCounties()
        {
            List<string> countiesList = new List<string>();
            var counties =  await _locationService.GetCounties();
            foreach ( var county in counties )
            {
                if ( county.Nume != null )
                    countiesList.Add(county.Nume);
            }
            return countiesList;
        }


        [HttpGet]
        [Route("cities/{country}")]
        public async Task<List<string>> GetCities(string country)
        {
            List<string> cityList = new List<string>();
            var cities = await _locationService.GetCitiesInCounty(country);
            foreach ( var city in cities )
            {
                if ( city.Nume != null )
                {
                    cityList.Add(city.Nume);
                }
            }

            cityList.Sort();
            return cityList;
        }


        [HttpGet]
        [Route("GetAuto/{county}")]
        public async Task<string> GetCountyAuto(string county)
        {
            List<County> counties = await _locationService.GetCounties();
            foreach ( var c in counties )
            {
                if ( c.Nume == county )
                {
                    return c.Auto;
                }
            }
            return "notfound";
        }


    }


}