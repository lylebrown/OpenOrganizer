using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                dataItems = dataContext.Items
                    .Include(item => item.Category)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .Include(item => item.Location)
                        .ThenInclude(loc => loc.Parent)
                            .ThenInclude(loc => loc.Parent)
                    .ToList();

                return dataItems;
            }
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemItem = dataContext.Items
                    .Include(item => item.Category)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .Include(item => item.Location)
                        .ThenInclude(loc => loc.Parent)
                            .ThenInclude(loc => loc.Parent)
                    .SingleOrDefault(x => x.ID == id);

                if (itemItem == null)
                {
                    return NotFound();
                }

                return itemItem;
            }
        }

        // POST api/items
        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);
                int locationQuery = Convert.ToInt32(HttpContext.Request.Query["Location"]);

                item.Category = dataContext.Categories.Find(categoryQuery);
                item.Location = dataContext.Locations.Find(locationQuery);
                dataContext.Items.Add(item);
                dataContext.SaveChanges();
                return Ok(item);
            }
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            // TODO: Add data validation
            item.ID = id;
            using (var dataContext = new APIDBContext())
            {
                int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);
                int locationQuery = Convert.ToInt32(HttpContext.Request.Query["Location"]);

                item.Category = dataContext.Categories.Find(categoryQuery);
                item.Location = dataContext.Locations.Find(locationQuery);
                dataContext.Items.Update(item);
                dataContext.SaveChanges();
                return Ok(item);
            }
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Item item = new Item();
            item.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Items.Remove(item);
                dataContext.SaveChanges();
                return Ok();
            }
        }
    }
}
