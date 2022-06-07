using Microsoft.AspNetCore.Diagnostics;
using Pokemon.Data.DataAccess.Exceptions;

namespace Pokemon.Web.API.Middleware;

public static class PokemonErrorHandler
{
    public static void HandleErrors(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ResourceNotFoundException exception)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync($"Unable to find {exception.Resource} with identifier {exception.Identifier}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync("Unknown Error");
            }
        });
    }
}