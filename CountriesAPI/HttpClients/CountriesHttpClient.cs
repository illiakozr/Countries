using CountriesAPI.Interfaces.HttpClients;
using CountriesAPI.ViewModels.CountryModel;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace CountriesAPI.HttpClients
{
    public class CountriesHttpClient : ICountriesHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountriesHttpClient> _logger;

        public CountriesHttpClient(HttpClient httpClient, ILogger<CountriesHttpClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Country>?> GetCountriesAsync()
        {
            _httpClient.BaseAddress = new Uri("https://restcountries.com/v3.1/");
            _httpClient.DefaultRequestHeaders.Add(
               HeaderNames.Accept, "application/json");

            try
            {
                var httpResponse = await _httpClient.GetAsync("all");
                if (httpResponse.IsSuccessStatusCode)
                {
                    using var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    return JsonSerializer.Deserialize<List<Country>>(contentStream, options);
                }
                else
                {
                    _logger.LogError("Restcountries API returned error: ", httpResponse);
                    throw new Exception("Restcountries API returned error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured in GetCountriesAsync during countries retrival: ", ex);
                throw;
            }
        }
    }
}
