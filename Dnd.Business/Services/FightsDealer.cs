using System.Text;
using Dnd.Business.Controllers;
using Dnd.Business.Models;

namespace Dnd.Business.Services;

public static class FightsDealer
{
    public static FightController.FightResult GetFightLog(Character player, Character monster)
    {
        var stringBuilder = new StringBuilder();
        for (;;)
        {
            if (CheckHps(player, monster, stringBuilder))
                break;
            if(Attack(player, monster, stringBuilder))
                break;
            if (CheckHps(player, monster, stringBuilder))
                break;
            if (Attack(monster, player, stringBuilder))
                break;
        }

        return new FightController.FightResult(stringBuilder.ToString(), player);
    }

    private static bool Attack(Character character1, Character character2, StringBuilder stringBuilder)
    {
        for (var i = 0; i < character1.AttackPerRound; i++)
        {
            var random = Random.Shared.Next(20) + 1;
            var modifiers = character1.AttackModifier + character1.Weapon;
            stringBuilder.Append($"{character1.Name} выкинул {random}(+{modifiers}) на атаку\r\n");
            if (random == 20)
                stringBuilder.Append("крит\r\n");
            if (random + modifiers <= character2.Ac) continue;
            stringBuilder.Append($"больше {character2.Ac}\r\n");
            stringBuilder.Append("попал\r\n");
            var damageRandom = 0;
            for (var j = 0; j < character1.Damage; ++j)
                damageRandom += Random.Shared.Next(character1.DiceType) + 1;
            var damageModifiers = character1.Weapon + character1.DamageModifier;
            stringBuilder.Append($"выкинул {damageRandom}(+{damageModifiers}) на дамаг\r\n");
            character2.HitPoints -= (damageRandom + damageModifiers) * (random == 20 ? 2 : 1);
            character2.HitPoints = Math.Max(0, character2.HitPoints);
            stringBuilder.Append(
                $"{character2.Name} теряет {(damageRandom + damageModifiers) * (random == 20 ? 2 : 1)} хп, осталось {character2.HitPoints}\r\n");
            if (CheckHps(character1, character2, stringBuilder))
                return true;
        }

        return false;
    }

    private static bool CheckHps(Character player, Character monster, StringBuilder stringBuilder)
    {
        if (player.HitPoints <= 0)
        {
            stringBuilder.Append($"{monster.Name} выиграл\r\n");
            return true;
        }

        if (monster.HitPoints <= 0)
        {
            stringBuilder.Append($"{player.Name} выиграл\r\n");
            return true;
        }

        return false;
    }
}