using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pokemon.Web.API.Filters;

public class PokemonUpdateFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var pokemon = context.ActionArguments.SingleOrDefault(x => x.Value is Data.Models.Pokemon);
        if (pokemon.Value is null)
        {
            context.Result = new BadRequestObjectResult("No valid object in body");
        }

        var modelStateEntryId = context.ModelState["id"];

        if (int.Parse(modelStateEntryId.AttemptedValue) != (pokemon.Value as Data.Models.Pokemon).Id)
        {
            context.Result = new BadRequestObjectResult($"route and object data mismatch");
        }

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}