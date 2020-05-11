using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Pokemon.Data.DataAccess;
using Testing.Pokemon.Data.Entities;

namespace Testing.Pokemon.Services
{
    public class PokedexServices
    {
        // Dependency Injected Items
        private readonly PokeContext _pokeContext;
        private readonly ILogger _logger;

        // Lazy Loaded Services
        private PokemonNameService _pokemonNameService;
        private PokemonService _pokemonService;
        private BaseStatService _baseStatService;

        public PokedexServices(PokeContext pokeContext, ILogger logger)
        {
            _pokeContext = pokeContext ?? throw new ArgumentNullException(nameof(pokeContext));
            _logger = logger;
        }

        public PokemonService PokemonService => _pokemonService = _pokemonService ?? new PokemonService(_pokeContext, _logger);
        public PokemonNameService PokemonNameService => _pokemonNameService = _pokemonNameService ?? new PokemonNameService(_pokeContext, _logger);
        public BaseStatService BaseStatService => _baseStatService = _baseStatService ?? new BaseStatService(_pokeContext, _logger);
    }
}
