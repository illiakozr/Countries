using CountriesAPI.ViewModels.CountryModel;
using CountriesAPI.ViewModels;

namespace CountriesAPI.Interfaces.Services
{
    public interface ICountriesService
    {
        Task<List<Country>> GetCountries(CountriesRequestModel requestModel);
    }
}
