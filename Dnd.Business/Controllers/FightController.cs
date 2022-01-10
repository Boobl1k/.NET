using Dnd.Business.Models;
using Dnd.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Business.Controllers;

[ApiController]
[Route("[action]")]
public class FightController
{
    public record FightInput(Character Player, Character Monster);
    private record FightResult(string Log);
    [HttpPost]
    public IActionResult Fight(FightInput input)
    {
        var (player, monster) = input;
        return new JsonResult(new FightResult(FightsDealer.GetFightLog(player, monster)));
    }
}