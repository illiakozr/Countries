﻿using CountriesAPI.Helpers;
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
            allCountires = SortCountries(allCountires, requestModel);
            allCountires = TakeFirstNCountires(allCountires, requestModel);

            return allCountires;
        }

        private static List<Country> FilterCountries(List<Country> countries, CountriesRequestModel requestModel)
        {
            return countries.AsQueryable()
                            .WhereIf(!string.IsNullOrEmpty(requestModel.Name), c => c.Name.Common.Contains(requestModel.Name, StringComparison.OrdinalIgnoreCase))
                            .WhereIf(requestModel.Population != default, c => c.Population < requestModel.Population * 1000000).ToList();
        }

        private static List<Country> SortCountries(List<Country> countries, CountriesRequestModel requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Sort))
            {
                if (requestModel.Sort.Equals("ascend", StringComparison.OrdinalIgnoreCase))
                {
                    return countries.OrderBy(c => c.Name.Common).ToList();
                }
                if (requestModel.Sort.Equals("descend", StringComparison.OrdinalIgnoreCase))
                {
                    return countries.OrderByDescending(c => c.Name.Common).ToList();
                }
            }

            return countries;
        }

        private static List<Country> TakeFirstNCountires(List<Country> countries, CountriesRequestModel requestModel)
        {
            if (requestModel.PageSize != default)
            {
                return countries.Take(requestModel.PageSize).ToList();
            }

            return countries;
        }
    }
}
