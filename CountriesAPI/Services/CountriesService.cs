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

            allCountires = FilterCountries(allCountires, requestModel);

            return allCountires;
        }

        private List<Country> FilterCountries(List<Country> countries, CountriesRequestModel requestModel)
        {
            IEnumerable<Country> filteredCountires = countries.AsEnumerable();
            if (!string.IsNullOrEmpty(requestModel.Name))
            {
                filteredCountires = countries.Where(c => c.Name.Common.Contains(requestModel.Name, StringComparison.OrdinalIgnoreCase));
            }

            return filteredCountires.ToList();
        }
    }
}
