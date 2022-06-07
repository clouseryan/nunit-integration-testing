using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pokemon.Data.Entities;

public class PokemonName
{
    public int PokemonId { get; set; }
    public string English { get; set; }
    public string Japanese { get; set; }
    public string Chinese { get; set; }
    
    [ForeignKey(nameof(PokemonId))]
    public Pokemon Pokemon { get; set; }

    public void Update(PokemonName name)
    {
        English = name.English;
        Japanese = name.Japanese;
        Chinese = name.Chinese;
    }
}