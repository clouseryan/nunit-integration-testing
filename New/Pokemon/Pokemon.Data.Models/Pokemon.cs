using System.Text.Json.Serialization;

namespace Pokemon.Data.Models;

public class Pokemon
{
    [JsonPropertyName("pokemonId")]
    public int Id { get; set; }

    [JsonPropertyName("pokemonTypes")]
    public List<string> Types { get; set; }
    
    [JsonPropertyName("englishName")]
    public string EnglishName { get; set; }

    [JsonPropertyName("japaneseName")]
    public string JapaneseName { get; set; }
    
    [JsonPropertyName("chineseName")]
    public string ChineseName { get; set; }

    [JsonPropertyName("HP")]
    public int HitPoints { get; set; }
    
    [JsonPropertyName("attack")]
    public int Attack { get; set; }
    
    [JsonPropertyName("defense")]
    public int Defense { get; set; }
    
    [JsonPropertyName("spAttack")]
    public int SpecialAttack { get; set; }
    
    [JsonPropertyName("spDefense")]
    public int SpecialDefense { get; set; }
    
    [JsonPropertyName("speed")]
    public int Speed { get; set; }
}