using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenOrganizerAPI.Models;

namespace OpenOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            //return new string[] { "value1", "value2" };
            List<Category> myList = new List<Category>();
            using (var dataContext = new APIDBContext())
            {
                dataContext.Categories.Add(new Models.Category() { Name = "Car" });
                dataContext.Categories.Add(new Models.Category() { Name = "Cables" });
                dataContext.Categories.Add(new Models.Category() { Name = "Electronics" });

                dataContext.SaveChanges();

                myList = dataContext.Categories.AsQueryable().ToList();

                return myList;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
