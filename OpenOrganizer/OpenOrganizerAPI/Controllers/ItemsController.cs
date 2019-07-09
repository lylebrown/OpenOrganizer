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
    public class ItemsController : ControllerBase
    {
        // GET api/items
        [HttpGet]
        public ActionResult<List<Item>> Get()
        {
            List<Item> dataItems = new List<Item>();
            using (var dataContext = new APIDBContext())
            {
                dataItems = dataContext.Items.AsQueryable().ToList();

                return dataItems;
            }
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemItem = dataContext.Items.Find(id);

                if (itemItem == null)
                {
                    return NotFound();
                }

                return itemItem;
            }
        }

        // POST api/items
        [HttpPost]
        public void Post([FromBody] Item item)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.Items.Add(item);
                dataContext.SaveChanges();
            }
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item item)
        {
            // TODO: Add data validation
            item.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Items.Update(item);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Item item = new Item();
            item.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Items.Remove(item);
                dataContext.SaveChanges();
            }
        }
    }
}
