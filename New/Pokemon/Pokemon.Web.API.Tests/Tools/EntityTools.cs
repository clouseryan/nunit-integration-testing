namespace Pokemon.Web.API.Tests.Tools;

public static class EntityTools
{
    public static Data.Entities.Pokemon BuildPokemon(
        int id = 1,
        List<string>? types = null,
        Data.Entities.PokemonName? pokemonName = null,
        Data.Entities.BaseStat? baseStat = null)
    {
        return new Data.Entities.Pokemon()
        {
            Id = id,
            Types = types ?? new List<string>() { "Grass" },
            Name = pokemonName ?? BuildPokemonName(id: id),
            BaseStat = baseStat ?? BuildBaseStat(id: id)
        };
    }

    public static Data.Entities.BaseStat BuildBaseStat(
        int id = 1,
        int hitPoints = 30,
        int attack = 10,
        int defense = 10,
        int spAttack = 12,
        int spDefense = 8,
        int speed = 10)
    {
        return new Data.Entities.BaseStat()
        {
            PokemonId = id,
            HitPoints = hitPoints,
            Attack = attack,
            Defense = defense,
            SpAttack = spAttack,
            SpDefense = spDefense,
            Speed = speed
        };
    }

    public static Data.Entities.PokemonName BuildPokemonName(
        int id = 1,
        string english = "Bulbasaur",
        string chinese = "妙蛙種子 / 妙蛙种子",
        string japanese = "しょくぶつポケモン")
    {
        return new Data.Entities.PokemonName()
        {
            PokemonId = id,
            English = english,
            Chinese = chinese,
            Japanese = japanese
        };
    }
}