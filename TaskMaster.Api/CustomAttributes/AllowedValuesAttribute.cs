using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Api.CustomAttributes;

public class AllowedValuesAttribute : ValidationAttribute
{
    public string[] Values { get; set; }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || Values.Contains(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var msg = $"{value} is not one of the allowable values: {string.Join(", ", (Values ?? new string[] { "No allowable values found" }))}.";
        return new ValidationResult(msg);
    }
}