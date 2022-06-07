namespace Pokemon.Web.API.Tests.Tools;

public static class ModelTools
{
    public static Data.Models.Pokemon BuildPokemon(
        int id = 2,
        List<string>? types = null,
        string englishName = "Squirtle",
        string chineseName = "杰尼龟",
        string japaneseName = "ゼニガメ",
        int hitPoints = 30,
        int attack = 10,
        int defense = 10,
        int spAttack = 12,
        int spDefense = 8,
        int speed = 10)
    {
        return new Data.Models.Pokemon()
        {
            Id = id,
            Types = types ?? new List<string>() { "Water" },
            EnglishName = englishName,
            ChineseName = chineseName,
            JapaneseName = japaneseName,
            HitPoints = hitPoints,
            Attack = attack,
            Defense = defense,
            SpecialAttack = spAttack,
            SpecialDefense = spDefense,
            Speed = speed
        };
    }
}