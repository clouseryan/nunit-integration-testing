using System.ComponentModel.DataAnnotations;

namespace Pokemon.Data.Entities;

public class BaseStat
{
    [Key]
    public int PokemonId { get; set; }
    public int HitPoints { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpAttack { get; set; }
    public int SpDefense { get; set; }
    public int Speed { get; set; }
    
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