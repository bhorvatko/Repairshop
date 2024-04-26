using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Repairshop.Client.Common.Validation;

public class NotEmptyAttribute
    : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, 
        ValidationContext validationContext)
    {
        IEnumerable property = (IEnumerable)value!;

        int count = 0;

        foreach (var item in property) count++;

        if (count > 0) return ValidationResult.Success;

        return new ValidationResult("Collection must not be empty.");
    }
}
