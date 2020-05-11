using System;
using System.Collections.Generic;
using System.Text;
using Testing.Pokemon.Data.Entities;
using Entities = Testing.Pokemon.Data.Entities;

namespace Testing.Pokemon.Services.Tests
{
    /// <summary>
    /// Tools used to build entities used during integration testing
    /// </summary>
    public static class TestTools
    {
        public static Entities.Pokemon BuildPokemon(
            int id = 1,
            Entities.PokemonName name = null,
            List<string> types = null,
            BaseStat baseStat = null)
        {
            return new Entities.Pokemon()
            {
                Id = id,
                Name = name == null ? BuildPokemonName() : name,
                Types = types == null ? new List<string>() { "Grass" } : types,
                BaseStat = baseStat == null ? BuildBaseStat() : baseStat
            };
        }

        public static PokemonName BuildPokemonName(
            int pokemonId = 1,
            string english = "Bulbasaur",
            string japanese = "フシギダネ",
            string chinese = "妙蛙种子")
        {
            return new PokemonName()
            {
                PokemonId = pokemonId,
                English = english,
                Japanese = japanese,
                Chinese = chinese
            };
        }

        public static BaseStat BuildBaseStat(
            int pokemonId = 1,
            int hitPoints = 45,
            int attack = 49,
            int defense = 49,
            int specialAttack = 65,
            int specialDefense = 65,
            int speed = 45)
        {
            return new BaseStat()
            {
                PokemonId = pokemonId,
                HitPoints = hitPoints,
                Attack = attack,
                Defense = defense,
                SpAttack = specialAttack,
                SpDefense = specialDefense,
                Speed = speed
            };
        }
    }
}
