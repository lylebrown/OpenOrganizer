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
        private readonly APIDBContext db;
        public ItemsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/items
        [HttpGet]
        public ActionResult<List<Item>> Get()
        {
            return db.Items
                .Include(item => item.Category)
                    .ThenInclude(cat => cat.Parent)
                        .ThenInclude(cat => cat.Parent)
                .Include(item => item.Location)
                    .ThenInclude(loc => loc.Parent)
                        .ThenInclude(loc => loc.Parent)
                .ToList();    
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var itemItem = db.Items
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

        // POST api/items
        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            // TODO: Add data validation
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);
            int locationQuery = Convert.ToInt32(HttpContext.Request.Query["Location"]);

            item.Category = db.Categories.Find(categoryQuery);
            item.Location = db.Locations.Find(locationQuery);
            db.Items.Add(item);
            db.SaveChanges();
            return Ok(item);
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            // TODO: Add data validation
            item.ID = id;
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);
            int locationQuery = Convert.ToInt32(HttpContext.Request.Query["Location"]);

            item.Category = db.Categories.Find(categoryQuery);
            item.Location = db.Locations.Find(locationQuery);
            db.Items.Update(item);
            db.SaveChanges();
            return Ok(item);
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Item item = new Item();
            item.ID = id;
            db.Items.Remove(item);
            db.SaveChanges();
            return Ok();
        }
    }
}
