using Dnd.Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Db.Controllers;

[ApiController]
[Route("[action]")]
public class MonstersController
{
    [HttpGet]
    public IActionResult GetRandomMonster([FromServices] QweContext dataContext)
    {
        if (dataContext.Monsters.Count() is 0)
        {
            dataContext.Monsters.Add(new Monster
            {
                Name = "Дерголот",
                HitPoints = 119,
                AttackModifier = 6,
                AttackPerRound = 2,
                Damage = 2,
                DiceType = 8,
                DamageModifier = 3,
                Weapon = 0,
                Ac = 15
            });
            dataContext.Monsters.Add(new Monster
            {
                Name = "Лич",
                HitPoints = 135,
                AttackModifier = 12,
                AttackPerRound = 1,
                Damage = 3,
                DiceType = 6,
                DamageModifier = 0,
                Weapon = 0,
                Ac = 17
            });
            dataContext.Monsters.Add(new Monster
            {
                Id = 0,
                Name = "Магмин",
                HitPoints = 9,
                AttackModifier = 4,
                AttackPerRound = 1,
                Damage = 2,
                DiceType = 6,
                DamageModifier = 0,
                Weapon = 0,
                Ac = 14
            });
            dataContext.SaveChanges();
        }

        var random = Random.Shared.Next(dataContext.Monsters.Count());
        return new JsonResult((random > 0 ? dataContext.Monsters.Skip(random) : dataContext.Monsters).First());
    }
}