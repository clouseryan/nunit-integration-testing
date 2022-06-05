using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Testing.Pokemon.Data.DataAccess;

namespace Testing.Pokemon.Services
{
    public class PokemonService : BaseService
    {
        public PokemonService(PokeContext pokeContext, ILogger logger) : base(pokeContext, logger)
        {
        }

        public async Task<Data.Entities.Pokemon> UpSert(Data.Entities.Pokemon pokemon)
        {
            if (pokemon.Id == 0)
                return await Insert(pokemon);

            return await Update(pokemon);
        }

        public async Task<Data.Entities.Pokemon> Get(int id)
        {
            return await _context.Pokemon.SingleAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Data.Entities.Pokemon>> Get(IEnumerable<string> names)
        {
            return await _context.Pokemon
                .Where(x => names.Contains(x.Name.English))
                .ToListAsync();
        }

        public async Task Delete(int id)
        {
            var existingPokemon = await _context.Pokemon.SingleOrDefaultAsync(x => x.Id == id);

            if (existingPokemon != null)
            {
                _context.Pokemon.Remove(existingPokemon);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Data.Entities.Pokemon> Update(Data.Entities.Pokemon pokemon)
        {
            var existingPokemon = await _context.Pokemon
                .Include(x => x.BaseStat)
                .Include(x => x.Name)
                .SingleOrDefaultAsync(x => x.Id == pokemon.Id);

            if (existingPokemon == null)
            {
                _logger.LogError($"Pokemon does not exist with Id :: {pokemon.Id}");
                throw new Exception($"Pokemon does not exist with Id :: {pokemon.Id}");
            }

            existingPokemon.Update(pokemon);

            await _context.SaveChangesAsync();

            return existingPokemon;
        }

        private async Task<Data.Entities.Pokemon> Insert(Data.Entities.Pokemon pokemon)
        {
            await _context.Pokemon.AddAsync(pokemon);
            await _context.SaveChangesAsync();

            return pokemon;
        }
    }
}
