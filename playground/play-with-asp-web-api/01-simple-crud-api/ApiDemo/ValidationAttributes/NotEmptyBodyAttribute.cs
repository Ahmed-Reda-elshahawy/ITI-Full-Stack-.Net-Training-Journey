using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ApiDemo.Validation;

public class NotEmptyBodyAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object value)
    {
        return value != null;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-required", ErrorMessage ?? "Request body cannot be empty.");
    }
}
