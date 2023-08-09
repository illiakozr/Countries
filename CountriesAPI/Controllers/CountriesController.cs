using CountriesAPI.Interfaces.Services;
using CountriesAPI.ViewModels;
using CountriesAPI.ViewModels.CountryModel;
using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService ?? throw new ArgumentNullException(nameof(countriesService));
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries([FromQuery] CountriesRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                return await _countriesService.GetCountries(requestModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
