using CountriesAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.ViewModels
{
    public class CountriesRequestModel
    {
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "population value must be greater than 0 and type integer")]
        public int Population { get; set; }

        [SortValidation(ErrorMessage = $"sort value allowed only 'ascend' or 'descend'")]
        public string Sort { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "page size value must be greater than 0 and type integer")]
        public int PageSize { get; set; }
    }
}
