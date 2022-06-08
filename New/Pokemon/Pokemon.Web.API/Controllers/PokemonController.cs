using Microsoft.AspNetCore.Mvc;
using Pokemon.Services.Core;
using Pokemon.Web.API.Filters;

namespace Pokemon.Web.API.Controllers;

[Route("api/Pokemon")]
public class PokemonController : Controller
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken)
    {
        return Ok(await _pokemonService.GetPokemonById(id, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Data.Models.Pokemon newPokemon, CancellationToken cancellationToken)
    {
        var created = await _pokemonService.CreateOrUpdatePokemon(newPokemon, cancellationToken);
        return Created($"api/Pokemon{created.Id}", created);
    }
    
    [ServiceFilter(typeof(PokemonUpdateFilter))]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] Data.Models.Pokemon updatedPokemon,
        CancellationToken cancellationToken)
    {
        return Ok(await _pokemonService.CreateOrUpdatePokemon(updatedPokemon, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _pokemonService.DeletePokemon(id, cancellationToken);
        return NoContent();
    }
    
}