using CountriesAPI.Interfaces.HttpClients;
using CountriesAPI.Interfaces.Services;
using CountriesAPI.ViewModels;
using CountriesAPI.ViewModels.CountryModel;

namespace CountriesAPI.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesHttpClient _countriesHttpClient;

        public CountriesService(ICountriesHttpClient countriesHttpClient)
        {
            _countriesHttpClient = countriesHttpClient ?? throw new ArgumentNullException(nameof(countriesHttpClient));
        }

        public async Task<List<Country>> GetCountries(CountriesRequestModel requestModel)
        {
            var allCountires = await _countriesHttpClient.GetCountriesAsync();
            if (allCountires == null || !allCountires.Any())
            {
                return new List<Country>();
            }

            return allCountires;
        }
    }
}
