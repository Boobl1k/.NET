using Dnd.Business.Models;
using Dnd.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Business.Controllers;

[ApiController]
[Route("[action]")]
public class CharacterController
{
    [HttpPost]
    public IActionResult CalculateCharacter(Character character) => 
        new JsonResult(CharacterCalculator.Calculate(character));
}