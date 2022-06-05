using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Testing.Pokemon.Data.Entities
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public virtual PokemonName Name { get; set; }
        [JsonPropertyName("type")]
        public List<string> Types { get; set; }
        [JsonPropertyName("base")]
        public virtual BaseStat BaseStat { get; set; }

        public void Update(Pokemon pokemon)
        {
            if (pokemon.Name != null)
                this.Name.UpdatE(pokemon.Name);

            Types = pokemon.Types ?? this.Types;

            if (pokemon.BaseStat != null)
                this.BaseStat.Update(pokemon.BaseStat);
        }
    }
}
