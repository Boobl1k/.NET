using Dnd.Business.Models;
using Dnd.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Business.Controllers;

[ApiController]
[Route("[action]")]
public class FightController
{
    public record FightInput(Character Player, Character Monster);
    public record FightResult(string Log, Character player);
    [HttpPost]
    public IActionResult Fight(FightInput input)
    {
        if (input.Player.Name is null)
            input.Player.Name = string.Empty;
        var (player, monster) = input;
        return new JsonResult(FightsDealer.GetFightLog(player, monster));
    }
}