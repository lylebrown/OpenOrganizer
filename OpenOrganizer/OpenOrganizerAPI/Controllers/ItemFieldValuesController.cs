using Microsoft.AspNetCore.Mvc;
using OpenOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemFieldValuesController : ControllerBase
    {
        // GET api/itemfieldvalues
        [HttpGet]
        public ActionResult<List<ItemFieldValue>> Get()
        {
            List<ItemFieldValue> dataValues = new List<ItemFieldValue>();
            using (var dataContext = new APIDBContext())
            {
                dataValues = dataContext.ItemFieldValues.AsQueryable().ToList();

                return dataValues;
            }
        }

        // GET api/itemfieldvalues/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemFieldValue> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemValue = dataContext.ItemFieldValues.Find(id);

                if (itemValue == null)
                {
                    return NotFound();
                }

                return itemValue;
            }
        }

        // POST api/itemfieldvalues
        [HttpPost]
        public void Post([FromBody] ItemFieldValue value)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFieldValues.Add(value);
                dataContext.SaveChanges();
            }
        }

        // PUT api/itemfieldvalues/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemFieldValue value)
        {
            // TODO: Add data validation
            value.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFieldValues.Update(value);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/itemfieldvalues/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemFieldValue value = new ItemFieldValue();
            value.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFieldValues.Remove(value);
                dataContext.SaveChanges();
            }
        }
    }
}
