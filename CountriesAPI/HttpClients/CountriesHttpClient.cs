using CountriesAPI.Interfaces.HttpClients;
using CountriesAPI.ViewModels.CountryModel;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace CountriesAPI.HttpClients
{
    public class CountriesHttpClient : ICountriesHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountriesHttpClient> _logger;
        private readonly IConfiguration _configuration;

        public CountriesHttpClient(HttpClient httpClient,  ILogger<CountriesHttpClient> logger,
            IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Country>?> GetCountriesAsync()
        {
            _httpClient.BaseAddress = new Uri(_configuration["CountriesRestEndpoint"]);
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
                    throw new WebException("Restcountries API returned error");
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
