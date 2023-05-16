using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;





namespace ASPAPI_mongo.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        NamesDbContext myCollection = new NamesDbContext();

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<NameDetails> Get()
        {
            List<NameDetails> list = new List<NameDetails>();
            var nameQuery = from eachName in myCollection.Names
                            select new
                            {
                                eachName.Name1,
                                eachName.Gender.Gender1,
                                eachName.Origin.Usage,
                                eachName.YearMost,
                                eachName.YearLeast

                            };
            foreach (var item in nameQuery)
            {
                NameDetails details = new NameDetails();
                details.name = item.Name1;
                details.gender1 = item.Gender1;
                details.usage = item.Usage; 
                details.yearMost = item.YearMost;
                details.yearLeast = item.YearLeast;


                list.Add(details);
            }
            return list;

        }





        // POST api/<ValuesController>
        [HttpPost]
        public void Post(NameSubset nameValue)
        {
            Name newName = new Name();
            newName.Name1 = nameValue.name;
            newName.GenderId = nameValue.genderId;
            newName.OriginId = nameValue.originId;
            newName.YearMost = nameValue.yearMost;
            newName.YearLeast = nameValue.yearLeast;

            //These codes I missed
            myCollection.Add(newName);
            myCollection.SaveChanges();
            //---------------------
        }
        public class NameDetails
        {

            public string name { get; set; }
            public string gender1 { get; set; }
            public string usage { get; set; }
            public int yearMost { get; set; }
            public int yearLeast { get; set; }
        }

        public class NameSubset
        {

            public string name { get; set; }
            public int genderId { get; set; }
            public int originId { get; set; }
            public int yearMost { get; set; }
            public int yearLeast { get; set; }
        }


    }
}
