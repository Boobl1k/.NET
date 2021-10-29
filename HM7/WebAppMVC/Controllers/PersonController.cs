using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    [Route("Person")]
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Person() =>
            View();

        [HttpPost]
        public IActionResult Person(Person person) =>
            View(person);
    }
}
