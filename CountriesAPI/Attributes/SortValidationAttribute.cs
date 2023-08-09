using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.Attributes
{
    public class SortValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is string val &&
               (val.Equals("ascend", StringComparison.OrdinalIgnoreCase) ||
                val.Equals("descend", StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }
    }
}
