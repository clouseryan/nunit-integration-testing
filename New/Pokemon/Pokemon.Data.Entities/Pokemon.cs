using System.Text.Json.Serialization;

namespace Pokemon.Data.Entities;

public class Pokemon
{
    public int Id { get; set; }
    public virtual PokemonName Name { get; set; }
    public List<string> Types { get; set; }
    public virtual BaseStat BaseStat { get; set; }

    public void Update(Pokemon pokemon)
    {
        if (pokemon.Name != null)
            this.Name.Update(pokemon.Name);

        Types = pokemon.Types ?? this.Types;

        if (pokemon.BaseStat != null)
            this.BaseStat.Update(pokemon.BaseStat);
    }
}