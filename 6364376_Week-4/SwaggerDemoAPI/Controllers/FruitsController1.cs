using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FruitsController1 : ControllerBase
    {
        private static readonly List<string> fruits = new() 
        { 
            "Apple", "Banana", "Cherry", "Date", "Elderberry" 
        };

        [HttpGet]
        public IActionResult GetAllFruits()
        {
            return Ok(fruits);
        }

        [HttpGet("{index}")]
        public IActionResult GetFruitByIndex(int index)
        {
            if (index < 0 || index >= fruits.Count)
                return NotFound("Fruit not found");

            return Ok(fruits[index]);
        }
    }
}
