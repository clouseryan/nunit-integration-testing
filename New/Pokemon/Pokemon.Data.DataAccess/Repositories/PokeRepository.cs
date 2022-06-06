using Microsoft.EntityFrameworkCore;

namespace Pokemon.Data.DataAccess.Repositories;

public class PokeRepository
{
    private readonly PokeContext _context;
    
    public PokeRepository(PokeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Entities.Pokemon> GetById(int id)
    {
        return await _context.Pokemon
            .Where(p => p.Id == id)
            .Include(p => p.Name)
            .Include(p => p.BaseStat)
            .SingleAsync();
    }

    public async Task<Entities.Pokemon> Upsert(Entities.Pokemon upatedPokemon)
    {
        Entities.Pokemon existingPokemon = null;
        try
        {
            // will throw an exception if it does not exist
            existingPokemon = await GetById(upatedPokemon.Id);
        }
        catch (Exception e)
        {
            
        }

        if (existingPokemon is null)
        {
            existingPokemon = new Entities.Pokemon();
            await _context.Pokemon.AddAsync(existingPokemon);
        }
        
        existingPokemon.Update(upatedPokemon);
        await _context.SaveChangesAsync();
        
        return existingPokemon;
    }

    public async Task Delete(int id)
    {
        Entities.Pokemon existingPokemon = null;

        try
        {
            existingPokemon = await GetById(id);
            _context.Pokemon.Remove(existingPokemon);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            // do nothing on exception, it is already removed from the database
        }
    }
}