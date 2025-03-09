using System.ComponentModel.DataAnnotations;

namespace CvHantering.Services
{
    public static class ValidatorHelper
    {

        public static List<ValidationResult> ValidateInput<T>(T input)

        { 
            var validationContext = new ValidationContext(input);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(input, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
