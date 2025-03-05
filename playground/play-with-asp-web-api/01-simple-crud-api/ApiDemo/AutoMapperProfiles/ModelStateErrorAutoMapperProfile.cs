using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiDemo.AutoMapperProfiles;

public class ModelStateErrorAutoMapperProfile:Profile
{
    public ModelStateErrorAutoMapperProfile()
    {
        CreateMap<ModelStateDictionary, List<string>>()
            .ConvertUsing(modelState => modelState
                .Where(kv => kv.Value.Errors.Any()) // Only include fields with errors
                .SelectMany(kv => kv.Value.Errors.Select(e => e.ErrorMessage)) // Extract error messages
                .ToList());
    }
}
