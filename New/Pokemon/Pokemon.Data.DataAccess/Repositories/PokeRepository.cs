using Microsoft.EntityFrameworkCore;
using Pokemon.Data.DataAccess.Exceptions;

namespace Pokemon.Data.DataAccess.Repositories;

public class PokeRepository
{
    private readonly PokeContext _context;
    
    public PokeRepository(PokeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Entities.Pokemon> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Pokemon
                .Where(p => p.Id == id)
                .Include(p => p.Name)
                .Include(p => p.BaseStat)
                .SingleAsync(cancellationToken: cancellationToken);
        }
        catch (ArgumentNullException e)
        {
            throw new ResourceNotFoundException($"No resource found for id :: {id}", e, id.ToString(), "Pokemon");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Entities.Pokemon> Upsert(Entities.Pokemon updatedPokemon, CancellationToken cancellationToken)
    {
        Entities.Pokemon existingPokemon = null;
        try
        {
            // will throw an exception if it does not exist
            existingPokemon = await GetById(updatedPokemon.Id, cancellationToken);
        }
        catch (Exception e)
        {
            // ignored
        }

        if (existingPokemon is null)
        {
            existingPokemon = new Entities.Pokemon();
            await _context.Pokemon.AddAsync(existingPokemon, cancellationToken);
        }
        
        existingPokemon.Update(updatedPokemon);
        await _context.SaveChangesAsync(cancellationToken);
        
        return existingPokemon;
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var existingPokemon = await GetById(id, cancellationToken);
            _context.Pokemon.Remove(existingPokemon);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            // do nothing on exception, it is already removed from the database
        }
    }
}