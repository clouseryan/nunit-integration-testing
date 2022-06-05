using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pokemon.Data.Entities;

public class PokemonName
{
    public int PokemonId { get; set; }
    [JsonPropertyName("english")]
    public string English { get; set; }
    [JsonPropertyName("japanese")]
    public string Japanese { get; set; }
    [JsonPropertyName("chinese")]
    public string Chinese { get; set; }
    [ForeignKey("PokemonId")]
    public Pokemon Pokemon { get; set; }

    public void Update(PokemonName name)
    {
        this.English = name.English;
        this.Japanese = name.Japanese;
        this.Chinese = name.Chinese;
    }
}