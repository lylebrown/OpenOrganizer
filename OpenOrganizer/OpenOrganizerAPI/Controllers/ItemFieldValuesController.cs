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
    public class ItemFieldValuesController : ControllerBase
    {
        private readonly APIDBContext db;
        public ItemFieldValuesController(APIDBContext context)
        {
            db = context;
        }

        // GET api/itemfieldvalues
        [HttpGet]
        public IActionResult Get()
        {
            int limitQuery = Convert.ToInt32(HttpContext.Request.Query["Limit"]);
            int startingAfterQuery = Convert.ToInt32(HttpContext.Request.Query["Starting_After"]);
            // TODO: Add advanced search later, primitive search on all controllers like this for now.
            string searchQuery = HttpContext.Request.Query["Search"].ToString().ToLower();

            var searchItemNames = db.Items.Where(i => i.Title.ToLower().Contains(searchQuery)).Select(i => i.ID);
            var searchItemFields = db.ItemFields.Where(i => i.Name.ToLower().Contains(searchQuery)).Select(i => i.ID);

            var results = db.ItemFieldValues
                    .Include(item => item.Item)
                        .ThenInclude(item => item.Category)
                            .ThenInclude(cat => cat.Parent)
                                .ThenInclude(cat => cat.Parent)
                    .Include(itemField => itemField.ItemField)
                        .ThenInclude(itemField => itemField.Category)
                            .ThenInclude(cat => cat.Parent)
                                .ThenInclude(cat => cat.Parent)
                    .Where(result =>
                        searchItemNames.Contains(result.ID)
                        || searchItemFields.Contains(result.ID)
                        || result.Value.ToLower().Contains(searchQuery));

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

        // GET api/itemfieldvalues/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var itemValue = db.ItemFieldValues.Find(id);

            var results = db.ItemFieldValues
                   .Include(item => item.Item)
                       .ThenInclude(item => item.Category)
                           .ThenInclude(cat => cat.Parent)
                               .ThenInclude(cat => cat.Parent)
                   .Include(itemField => itemField.ItemField)
                       .ThenInclude(itemField => itemField.Category)
                           .ThenInclude(cat => cat.Parent)
                               .ThenInclude(cat => cat.Parent)
                    .SingleOrDefault(x => x.ID == id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(itemValue);
        }

        // POST api/itemfieldvalues
        [HttpPost]
        public IActionResult Post([FromBody] ItemFieldValue value)
        {
            // TODO: Add data validation
            int itemQuery = Convert.ToInt32(HttpContext.Request.Query["Item"]);
            int itemFieldQuery = Convert.ToInt32(HttpContext.Request.Query["ItemField"]);

            value.Item = db.Items.Find(itemQuery);
            value.ItemField = db.ItemFields.Find(itemFieldQuery);
            db.ItemFieldValues.Add(value);
            db.SaveChanges();
            return Ok(value.ID);
        }

        // PUT api/itemfieldvalues/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemFieldValue value)
        {
            // TODO: Add data validation
            int itemQuery = Convert.ToInt32(HttpContext.Request.Query["Item"]);
            int itemFieldQuery = Convert.ToInt32(HttpContext.Request.Query["ItemField"]);

            value.ID = id;
            value.Item = db.Items.Find(itemQuery);
            value.ItemField = db.ItemFields.Find(itemFieldQuery);

            db.ItemFieldValues.Update(value);
            db.SaveChanges();
            return Ok(value.ID);
        }

        // DELETE api/itemfieldvalues/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ItemFieldValue value = new ItemFieldValue();
            value.ID = id;
            db.ItemFieldValues.Remove(value);
            db.SaveChanges();
            return Ok();
        }
    }
}
