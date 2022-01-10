using Dnd.Business.Models;

namespace Dnd.Business.Services;

public static class CharacterCalculator
{
    public static CalculatedCharacter Calculate(Character character) =>
        new CalculatedCharacter
        {
            Name = character.Name,
            HitPoints = character.HitPoints,
            AttackModifier = character.AttackModifier,
            AttackPerRound = character.AttackPerRound,
            Damage = character.Damage,
            DiceType = character.DiceType,
            DamageModifier = character.DamageModifier,
            Weapon = character.Weapon,
            Ac = character.Ac,
            MinAcToAlwaysHit = character.AttackModifier + character.Weapon + 1,
            MinDamagePerRound = (character.Damage + character.Weapon + character.DamageModifier) *
                                character.AttackPerRound,
            MaxDamagePerRound = (character.Damage * character.DiceType + character.Weapon + character.DamageModifier) *
                                character.AttackPerRound
        };
}