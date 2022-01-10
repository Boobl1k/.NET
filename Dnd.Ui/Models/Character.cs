using System.ComponentModel.DataAnnotations;

namespace Dnd.Ui.Models;

public class Character
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int HitPoints { get; set; }
    [Required]
    public int AttackModifier { get; set; }
    [Required]
    public int AttackPerRound { get; set; }
    [Required]
    public int Damage { get; set; }
    [Required]
    public int DiceType { get; set; }
    [Required]
    public int DamageModifier { get; set; }
    [Required]
    public int Weapon { get; set; }
    [Required]
    public int Ac { get; set; }
}