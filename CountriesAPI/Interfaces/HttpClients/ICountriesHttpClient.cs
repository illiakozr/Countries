using CountriesAPI.ViewModels.CountryModel;

namespace CountriesAPI.Interfaces.HttpClients
{
    public interface ICountriesHttpClient
    {
        Task<List<Country>> GetCountriesAsync();
    }
}
