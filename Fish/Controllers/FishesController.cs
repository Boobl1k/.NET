using Microsoft.AspNetCore.Mvc;

namespace Fish.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FishesController : ControllerBase
{
    public IActionResult AddTaskFish() => 
        Aquarium.TryAddFish(true) ? Ok() : BadRequest();

    public IActionResult AddThreadFish() =>
        Aquarium.TryAddFish(false) ? Ok() : BadRequest();

    public IActionResult KillFish(int id) =>
        Aquarium.TryKillFish(id) ? Ok() : BadRequest();
}