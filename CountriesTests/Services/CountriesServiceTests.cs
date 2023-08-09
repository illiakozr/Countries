using CountriesAPI.Interfaces.HttpClients;
using CountriesAPI.Services;
using CountriesAPI.ViewModels;
using CountriesAPI.ViewModels.CountryModel;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CountriesTests.Services
{
    public class CountriesServiceTests
    {
        private readonly Mock<ICountriesHttpClient> _countriesHttpClient = new();
        private readonly CountriesService _countriesService;

        public CountriesServiceTests()
        {
            _countriesService = new CountriesService(_countriesHttpClient.Object);
        }

        [Fact]
        public async Task GetCountries_ReturnAllCountries_WhenNoFiltersProvided()
        {
            // Arrange
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(new CountriesRequestModel());

            // Assert
            Assert.Equal(expectedCountries.Count, result.Count);
            Assert.Equal(expectedCountries.First().Name.Common, result.First().Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnAllAscendSortedCountries_WhenSortFilterProvided()
        {
            // Arrange
            var requestModel = new CountriesRequestModel { Sort = "ascend" };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedCountries.Count, result.Count);
            Assert.Equal("Australia", result[0].Name.Common);
            Assert.Equal("Columbia", result[1].Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnAllDescendSortedCountries_WhenSortFilterProvided()
        {
            // Arrange
            var requestModel = new CountriesRequestModel { Sort = "descend" };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedCountries.Count, result.Count);
            Assert.Equal("Ukraine", result[0].Name.Common);
            Assert.Equal("Mauritius", result[1].Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnFirstNCountires_WhenPageSizeFilterProvided()
        {
            // Arrange
            var expectedNumberOfCountires = 3;
            var requestModel = new CountriesRequestModel { PageSize = 3 };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedNumberOfCountires, result.Count);
        }

        [Fact]
        public async Task GetCountries_ReturnFilteredCountriesByName_WhenJustFilterByNameProvided()
        {
            // Arrange
            var expectedNumberOfCountires = 2;
            var requestModel = new CountriesRequestModel { Name = "aU" };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedNumberOfCountires, result.Count);
            Assert.Equal("Australia", result.First().Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnFilteredAndSortedCountriesByName_WhenSortAndNameFiltersProvided()
        {
            // Arrange
            var expectedNumberOfCountires = 2;
            var requestModel = new CountriesRequestModel { Name = "aU", Sort = "descend" };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedNumberOfCountires, result.Count);
            Assert.Equal("Mauritius", result.First().Name.Common);
            Assert.Equal("Australia", result.Last().Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnFilteredCountriesByPopulation_WhenJustFilterByPopulationProvided()
        {
            // Arrange
            var expectedNumberOfCountires = 3;
            var requestModel = new CountriesRequestModel { Population = 40 };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedNumberOfCountires, result.Count);
            Assert.Equal("Columbia", result.First().Name.Common);
        }

        [Fact]
        public async Task GetCountries_ReturnFilteredCountriesByNameAndPopulation_WhenFiltersProvided()
        {
            // Arrange
            var expectedNumberOfCountires = 1;
            var requestModel = new CountriesRequestModel { Name = "aU", Population = 24 };
            var expectedCountries = new List<Country>
            {
                new Country { Name = new Name { Common = "Columbia" }, Population = 34534523 },
                new Country { Name = new Name { Common = "Australia" }, Population = 25687041 },
                new Country { Name = new Name { Common = "Mauritius" }, Population = 1265740 },
                new Country { Name = new Name { Common = "Germany" }, Population = 83240525 },
                new Country { Name = new Name { Common = "Ukraine" }, Population = 44134693 },
            };
            _countriesHttpClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countriesService.GetCountries(requestModel);

            // Assert
            Assert.Equal(expectedNumberOfCountires, result.Count);
            Assert.Equal("Mauritius", result.First().Name.Common);
        }
    }
}
