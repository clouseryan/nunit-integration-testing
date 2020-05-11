using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Pokemon.Data.DataAccess;

namespace Testing.Pokemon.Services
{
    public class PokemonNameService : BaseService
    {
        public PokemonNameService(PokeContext pokeContext, ILogger logger) : base(pokeContext, logger)
        {
        }
    }
}
