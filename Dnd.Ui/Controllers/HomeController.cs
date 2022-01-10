using Dnd.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ui.Controllers;

public class HomeController : Controller
{
    private static HttpClient _client = new();

    public IActionResult Index() =>
        View();


    private record FightStartingModel(Character Player, Character Monster);
    public record FightResult(string Log, Character player);

    public record FightModel(CalculatedCharacter Character, string Log, Character DamagedCharacter);

    public async Task<IActionResult> Fight(Character player)
    {
        Console.WriteLine(player.Name);
        var q = await _client.GetAsync("https://localhost:7099/GetRandomMonster");
        var monster = await q.Content.ReadFromJsonAsync<Character>();
        var w =
            await _client.PostAsync("https://localhost:7263/CalculateCharacter", JsonContent.Create(player));
        var calculated = await w.Content.ReadFromJsonAsync<CalculatedCharacter>();

        var e = await _client.PostAsync("https://localhost:7263/Fight",
            JsonContent.Create(new FightStartingModel(player, monster!)));
        var fightResult = await e.Content.ReadFromJsonAsync<FightResult>();
        return View(new FightModel(calculated!, fightResult!.Log, fightResult.player));
    }
}