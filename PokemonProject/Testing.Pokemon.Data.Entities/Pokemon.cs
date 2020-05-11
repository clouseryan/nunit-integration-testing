using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Testing.Pokemon.Data.Entities
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public PokemonName Name { get; set; }
        [JsonPropertyName("type")]
        public List<string> Types { get; set; }
        [JsonPropertyName("base")]
        public BaseStat BaseStat { get; set; }

        public virtual PokemonName PokemonName { get; set; }
        public virtual BaseStat BaseStats { get; set; }
    }
}
