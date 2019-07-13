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
    public class ItemTagsController : ControllerBase
    {
        private readonly APIDBContext db;
        public ItemTagsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/itemtags
        [HttpGet]
        public ActionResult<List<ItemTag>> Get()
        {
            return db.ItemTags.ToList();
        }

        // GET api/itemtags/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemTag> Get(int id)
        {
            var itemTag = db.ItemTags.Find(id);

            if (itemTag == null)
            {
                return NotFound();
            }

            return itemTag;
        }

        // POST api/itemtags
        [HttpPost]
        public void Post([FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            db.ItemTags.Add(tag);
            db.SaveChanges();
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            tag.ID = id;
            db.ItemTags.Update(tag);
            db.SaveChanges();
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemTag tag = new ItemTag();
            tag.ID = id;
            db.ItemTags.Remove(tag);
            db.SaveChanges();
        }
    }
}
