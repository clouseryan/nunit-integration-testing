using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pokemon.Data.Entities;

public class BaseStat
{
    public int PokemonId { get; set; }
    [JsonPropertyName("HP")]
    public int HitPoints { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    [JsonPropertyName("Sp_Attack")]
    public int SpAttack { get; set; }
    [JsonPropertyName("Sp_Defense")]
    public int SpDefense { get; set; }
    public int Speed { get; set; }

    [ForeignKey("PokemonId")]
    public virtual Pokemon Pokemon { get; set; }

    public void Update(BaseStat baseStat)
    {
        this.HitPoints = baseStat.HitPoints;
        this.Attack = baseStat.Attack;
        this.Defense = baseStat.Defense;
        this.SpAttack = baseStat.SpAttack;
        this.SpDefense = baseStat.SpDefense;
        this.Speed = baseStat.Speed;
    }
}