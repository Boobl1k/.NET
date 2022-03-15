using Microsoft.AspNetCore.Mvc;

namespace Fish.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FishesController : ControllerBase
{
    public IActionResult AddTaskFish([FromQuery] int speed) =>
        Aquarium.TryAddFish(true, speed) ? Ok() : BadRequest();

    public IActionResult AddThreadFish([FromQuery] int speed) =>
        Aquarium.TryAddFish(false, speed) ? Ok() : BadRequest();

    public IActionResult KillFish(int id) =>
        Aquarium.TryKillFish(id) ? Ok() : BadRequest();
}
