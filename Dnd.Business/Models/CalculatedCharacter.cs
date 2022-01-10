namespace Dnd.Business.Models;

public class CalculatedCharacter : Character
{
    public int MinAcToAlwaysHit { get; set; }
    public int MinDamagePerRound { get; set; }
    public int MaxDamagePerRound { get; set; }
}