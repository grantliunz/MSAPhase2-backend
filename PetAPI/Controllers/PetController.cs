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
            _client = clientFactory.CreateClient("pet");
        }

        public PetController()
        {
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

        /// <summary>
        /// This is endpoint takes no arguments and returns the list of pets
        /// </summary>
        /// <returns>The list of pets</returns>
        [HttpGet]
        [Route("getPets")]
        public async Task<ActionResult<Pet>> GetPets()
        {
            if (pets.Count() == 0)
            {
                var res = await _client.GetAsync("/204.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            return Ok(pets);
        }


        /// <summary>
        /// Gets the pet object of a specific ID
        /// </summary>
        /// <param name="id">The id of the pet, which must be a valid integer</param>
        /// <returns>The pet object</returns>
        [HttpGet]
        [Route("getPet")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = pets.Find(x=>x.Id == id);
            if(pet == null)
            {
                var res = await _client.GetAsync("/404.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            return Ok(pet);
        }


        /// <summary>
        /// Adds a pet object to the list of pets
        /// </summary>
        /// <param name="request">The pet object to be added</param>
        /// <returns>Ok response in the form of pet image</returns>
        [HttpPost]
        [Route("addPet")]
        public async Task<ActionResult<Pet>> AddPet(Pet request)
        {
            var pet = pets.Find(x => x.Id == request.Id);
            if (pet != null)
            {
                var res = await _client.GetAsync("/409.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            else
            {
                pets.Add(request);
                var res = await _client.GetAsync("/200.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }




        }

        /// <summary>
        /// Updates an existing pet object
        /// </summary>
        /// <param name="request">The pet object to be updated</param>
        /// <returns>THe updated list of pets</returns>
        [HttpPut]
        [Route("updatePet")]
        public async Task<ActionResult<Pet>> UpdatePet(Pet request)
        {
            var pet = pets.Find(x => x.Id == request.Id);
            if (pet == null)
            {
                var res = await _client.GetAsync("/404.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            pet.Name = request.Name;
            pet.Type = request.Type;
            pet.Age = request.Age;
            return Ok(pets);
        }

        /// <summary>
        /// Removes a pet of specific Id from list
        /// </summary>
        /// <param name="Id">The Id of the pet to be deleteed</param>
        /// <returns>The list of pets after deletion</returns>
        [HttpDelete]
        [Route("DeletePet")]
        public async Task<ActionResult<Pet>> DeletePet(int Id)
        {
            var pet = pets.Find(x => x.Id == Id);
            if (pet == null)
            {
                var res = await _client.GetAsync("/404.jpg");
                var content = await res.Content.ReadAsByteArrayAsync();
                return File(content, "image/jpeg");
            }
            pets.Remove(pet);
            return Ok(pets);
        }
    }
}
