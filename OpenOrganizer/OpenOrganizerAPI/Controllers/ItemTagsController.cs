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
    public class ItemTagsController : ControllerBase
    {
        private readonly APIDBContext db;
        public ItemTagsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/itemtags
        [HttpGet]
        public IActionResult Get()
        {
            int limitQuery = Convert.ToInt32(HttpContext.Request.Query["Limit"]);
            int startingAfterQuery = Convert.ToInt32(HttpContext.Request.Query["Starting_After"]);
            // TODO: Add advanced search later, primitive search on all controllers like this for now.
            string searchQuery = HttpContext.Request.Query["Search"].ToString().ToLower();

            var searchItemIds = db.Items.Where(i => i.Title.ToLower().Contains(searchQuery)).Select(i => i.ID);
            var searchTagIds = db.Items.Where(i => i.Title.ToLower().Contains(searchQuery)).Select(i => i.ID);

            var results = db.ItemTags
                    .Include(tag => tag.Item)
                        .ThenInclude(item => item.Title)
                    .Include(tag => tag.Tag)
                        .ThenInclude(tag => tag.Name)
                    .Where(result =>
                        searchItemIds.Contains(result.Item.ID)
                        || searchItemIds.Contains(result.Tag.ID)
                        || result.Item.Title.ToLower().Contains(searchQuery)
                        || result.Tag.Name.ToLower().Contains(searchQuery));

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

        // GET api/itemtags/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var itemTag = db.ItemTags
                    .Include(tag => tag.Item)
                        .ThenInclude(item => item.Title)
                    .Include(tag => tag.Tag)
                        .ThenInclude(tag => tag.Name)
                    .SingleOrDefault(x => x.ID == id);

            if (itemTag == null)
            {
                return NotFound();
            }

            return Ok(itemTag);
        }

        // POST api/itemtags
        [HttpPost]
        public IActionResult Post([FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            int itemQuery = Convert.ToInt32(HttpContext.Request.Query["Item"]);
            int tagQuery = Convert.ToInt32(HttpContext.Request.Query["Tag"]);

            tag.Item = db.Items.Find(itemQuery);
            tag.Tag = db.Tags.Find(tagQuery);
            db.ItemTags.Add(tag);
            db.SaveChanges();
            return Ok(tag.ID);
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            int itemQuery = Convert.ToInt32(HttpContext.Request.Query["Item"]);
            int tagQuery = Convert.ToInt32(HttpContext.Request.Query["Tag"]);

            tag.Item = db.Items.Find(itemQuery);
            tag.Tag = db.Tags.Find(tagQuery);
            tag.ID = id;
            db.ItemTags.Update(tag);
            db.SaveChanges();
            return Ok(tag.ID);
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ItemTag tag = new ItemTag();
            tag.ID = id;
            db.ItemTags.Remove(tag);
            db.SaveChanges();
            return Ok();
        }
    }
}
