using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new List<Pet>()
        {
            new Pet()
            {
                Id = 1,
                Name = "Cleo",
                Type = "Dog",
                Age = 3
            },
            new Pet()
            {
                Id = 1,
                Name = "Cleo",
                Type = "Dog",
                Age = 3
            }
        };



    }
}
