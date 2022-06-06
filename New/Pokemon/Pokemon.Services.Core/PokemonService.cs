using Microsoft.EntityFrameworkCore.Infrastructure;
using Pokemon.Data.DataAccess;
using Pokemon.Data.DataAccess.Repositories;

namespace Pokemon.Services.Core;

public class PokemonService
{
    private readonly PokeRepository _repository;


    public PokemonService(PokeRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Data.Models.Pokemon> GetPokemonById(int id)
    {
        
    }
}