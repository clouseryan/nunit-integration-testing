using Microsoft.AspNetCore.Mvc;
using Pokemon.Services.Core;

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
        return Ok(await _pokemonService.CreateOrUpdatePokemon(newPokemon, cancellationToken));
    }
    
    
}