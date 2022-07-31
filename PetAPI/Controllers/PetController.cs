using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly HttpClient _client;
        /// <summary />
        /// 

        [ActivatorUtilitiesConstructor]
        public PetController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("cat");
        }

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
                Id = 2,
                Name = "Tom",
                Type = "Cat",
                Age = 4
            }
        };


        [HttpGet]
        [Route("getPets")]
        public async Task<ActionResult<Pet>> GetPets()
        {
            return Ok(pets);
        }


        [HttpGet]
        [Route("getPet")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = pets.Find(x=>x.Id == id);
            if(pet == null)
            {
                var res = await _client.GetAsync("/404");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            return Ok(pet);
        }



        [HttpPost]
        [Route("addPet")]
        public async Task<ActionResult<Pet>> AddPet(Pet request)
        {
            var pet = pets.Find(x => x.Id == request.Id);
            if (pet != null)
            {
                var res = await _client.GetAsync("/409");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }

            pets.Add(request);
            return Ok(pets);
        }

        [HttpPut]
        [Route("updatePet")]
        public async Task<ActionResult<Pet>> UpdatePet(Pet request)
        {
            var pet = pets.Find(x => x.Id == request.Id);
            if (pet == null)
            {
                var res = await _client.GetAsync("/404");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            pet.Name = request.Name;
            pet.Type = request.Type;
            pet.Age = request.Age;
            return Ok(pets);
        }

        [HttpDelete]
        [Route("DeletePet")]
        public async Task<ActionResult<Pet>> DeletePet(int Id)
        {
            var pet = pets.Find(x => x.Id == Id);
            if (pet == null)
            {
                var res = await _client.GetAsync("/404");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            pets.Remove(pet);
            return Ok(pets);
        }
    }
}
