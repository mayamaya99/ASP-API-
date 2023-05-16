using ASPAPI_mongo.Models;
using ASPAPI_mongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoNamesController : ControllerBase
    {
        private readonly MongoNamesServices _namesService;
        public MongoNamesController(MongoNamesServices namesService) =>
        _namesService = namesService;

        [HttpGet]
        public async Task<List<MongoNames>> Get() =>
            await _namesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MongoNames>> Get(string id)
        {
            var name = await _namesService.GetAsync(id);
            if (name is null)
            {
                return NotFound();
            }
            return name;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProtoName newName)
        {
            MongoNames tempMongoName = new MongoNames();
            tempMongoName.name = newName.name;
            tempMongoName.gender1 = newName.gender1;
            tempMongoName.usage = newName.usage;
            tempMongoName.yearMost = newName.yearMost;
            tempMongoName.yearLeast = newName.yearLeast;
            await _namesService.CreateAsync(tempMongoName);
            return CreatedAtAction(nameof(Get), newName);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, MongoNames updatedName)
        {
            var name = await _namesService.GetAsync(id);
            if (name is null)
            {
                return NotFound();
            }
            updatedName.Id = name.Id;
            await _namesService.UpdateAsync(id, updatedName);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var name = await _namesService.GetAsync(id);
            if (name is null)
            {
                return NotFound();
            }
            await _namesService.RemoveAsync(id);
            return NoContent();
        }
    }

    public class ProtoName
    {
        public string name { get; set; }
        public string gender1 { get; set; }
        public string usage { get; set; }
        public int yearMost { get; set; }
        public int yearLeast { get; set; }
    }


}


   

