using Dnd.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ui.Controllers;

public class HomeController : Controller
{
    private static HttpClient _client = new();

    public IActionResult Index() =>
        View();


    private record FightStartingModel(Character Player, Character Monster);
    private record FightResult(string Log);

    public record FightModel(CalculatedCharacter Character, string Log);

    public async Task<IActionResult> Fight(Character player)
    {
        var q = await _client.GetAsync("https://localhost:7099/GetRandomMonster");
        var monster = await q.Content.ReadFromJsonAsync<Character>();
        var w =
            await _client.PostAsync("https://localhost:7263/CalculateCharacter", JsonContent.Create(player));
        var calculated = await w.Content.ReadFromJsonAsync<CalculatedCharacter>();

        var e = await _client.PostAsync("https://localhost:7263/Fight",
            JsonContent.Create(new FightStartingModel(player, monster!)));
        var log = (await e.Content.ReadFromJsonAsync<FightResult>())!.Log;
        return View(new FightModel(calculated!, log));
    }
}