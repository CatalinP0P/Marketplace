using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Marketplace.Interfaces
{
    public interface IRoLocationService
    {
        Task<List<County>> GetCounties();
        Task<List<City>> GetCities();
        Task<List<City>> GetCitiesInCounty(string county);
    }

    public class RoLocationService : IRoLocationService
    {
        private readonly HttpClient _httpClient;
        public RoLocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<City>> GetCities()
        {
            var url = "orase";
            var response = await _httpClient.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<City>>(body);
        }

        public async Task<List<City>> GetCitiesInCounty(string county)
        {
            var url = $"orase/{county}";
            var response = await _httpClient.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<City>>(body); 
        }

        public async Task<List<County>> GetCounties()
        {
            var url = "judete";
            var response = await _httpClient.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<County>>(body);            
        }
    }

}