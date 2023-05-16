using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Drawing;



namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginController : ControllerBase
    {
        NamesDbContext myCollection = new NamesDbContext();

        // GET: api/<Origin>
        [HttpGet]
        public IEnumerable<Origin> Get()
        {
            List<Origin> list = new List<Origin>();
            list = myCollection.Origins.ToList();
            return list;
        }



        // POST api/<Origin>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Origin newOrigin = new Origin();
            newOrigin.Usage = value;
            myCollection.Origins.Add(newOrigin);
            myCollection.SaveChanges();
        }
    }


}

