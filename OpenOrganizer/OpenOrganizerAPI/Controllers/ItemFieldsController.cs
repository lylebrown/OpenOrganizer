using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Controllers
{
    [Route("api/v1/[controller]")]
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
        public IActionResult Get()
        {
            int limitQuery = Convert.ToInt32(HttpContext.Request.Query["Limit"]);
            int startingAfterQuery = Convert.ToInt32(HttpContext.Request.Query["Starting_After"]);
            // TODO: Add advanced search later, primitive search on all controllers like this for now.
            string searchQuery = HttpContext.Request.Query["Search"].ToString().ToLower();

            var searchCategoryIds = db.ItemFields.Where(c => c.Name.ToLower().Contains(searchQuery)).Select(c => c.ID);

            var results = db.ItemFields
                    .Include(cat => cat.Category)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .Where(result =>
                        searchCategoryIds.Contains(result.Category.ID)
                        || result.Name.ToLower().Contains(searchQuery));

            if (startingAfterQuery > 0)
            {
                results = results.Skip(startingAfterQuery);
            }
            if (limitQuery > 0)
            {
                results = results.Take(limitQuery);
            }


            return Ok(results);
        }

        // GET api/itemfields/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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

            return Ok(itemFieldItem);
        }

        // POST api/itemfields
        [HttpPost]
        public IActionResult Post([FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);

            itemField.Category = db.Categories.Find(categoryQuery);
            db.ItemFields.Add(itemField);
            db.SaveChanges();
            return Ok(itemField.ID);
        }

        // PUT api/itemfields/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);
            itemField.Category = db.Categories.Find(categoryQuery);

            itemField.ID = id;
            db.ItemFields.Update(itemField);
            db.SaveChanges();
            return Ok(itemField.ID);
        }

        // DELETE api/itemfields/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ItemField itemField = new ItemField();
            itemField.ID = id;
            db.ItemFields.Remove(itemField);
            db.SaveChanges();
            return Ok();
        }
    }
}
