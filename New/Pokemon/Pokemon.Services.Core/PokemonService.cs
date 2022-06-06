using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pokemon.Data.DataAccess;
using Pokemon.Data.DataAccess.Repositories;

namespace Pokemon.Services.Core;

public class PokemonService
{
    private readonly PokeRepository _repository;
    private readonly IMapper _mapper;


    public PokemonService(PokeRepository repository, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Data.Models.Pokemon> GetPokemonById(int id)
    {
        var entity = await _repository.GetById(id);
        return _mapper.Map<Data.Entities.Pokemon, Data.Models.Pokemon>(entity);
    }
}