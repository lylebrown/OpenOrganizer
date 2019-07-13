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
    public class ItemFieldsController : ControllerBase
    {
        private readonly APIDBContext db;
        public ItemFieldsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/itemfields
        [HttpGet]
        public ActionResult<List<ItemField>> Get()
        {
            return db.ItemFields
                .Include(field => field.Category)
                    .ThenInclude(cat => cat.Parent)
                        .ThenInclude(cat => cat.Parent)
                .ToList();
        }

        // GET api/itemfields/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemField> Get(int id)
        {
            var itemFieldItem = db.ItemFields
                .Include(field => field.Category)
                    .ThenInclude(cat => cat.Parent)
                        .ThenInclude(cat => cat.Parent)
                .SingleOrDefault(x => x.ID == id);

            if (itemFieldItem == null)
            {
                return NotFound();
            }

            return itemFieldItem;
        }

        // POST api/itemfields
        [HttpPost]
        public void Post([FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);

            itemField.Category = db.Categories.Find(categoryQuery);
            db.ItemFields.Add(itemField);
            db.SaveChanges();
        }

        // PUT api/itemfields/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            itemField.ID = id;
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);

            itemField.Category = db.Categories.Find(categoryQuery);
            db.ItemFields.Update(itemField);
            db.SaveChanges();
        }

        // DELETE api/itemfields/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemField itemField = new ItemField();
            itemField.ID = id;
            db.ItemFields.Remove(itemField);
            db.SaveChanges();
        }
    }
}
