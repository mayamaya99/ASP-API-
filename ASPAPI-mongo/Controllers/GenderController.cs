using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;



namespace ASPAPI_mongo.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    
    public class GenderController : ControllerBase
    {

        NamesDbContext myCollection = new NamesDbContext();

        // GET: api/<GenderController>
        [HttpGet]
        public IEnumerable<Gender> Get()
        {
            return myCollection.Genders;
        }



        // PUT api/<GenderController>/5
        // POST api/<GenreController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Gender newGender = new Gender();
            newGender.Gender1 = value;
            myCollection.Genders.Add(newGender);
            myCollection.SaveChanges();
        }
    }

}

