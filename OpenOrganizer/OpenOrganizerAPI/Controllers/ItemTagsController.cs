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
        // GET api/itemtags
        [HttpGet]
        public ActionResult<List<ItemTag>> Get()
        {
            List<ItemTag> dataTags = new List<ItemTag>();
            using (var dataContext = new APIDBContext())
            {
                dataTags = dataContext.ItemTags.AsQueryable().ToList();

                return dataTags;
            }
        }

        // GET api/itemtags/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemTag> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemTag = dataContext.ItemTags.Find(id);

                if (itemTag == null)
                {
                    return NotFound();
                }

                return itemTag;
            }
        }

        // POST api/itemtags
        [HttpPost]
        public void Post([FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemTags.Add(tag);
                dataContext.SaveChanges();
            }
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemTag tag)
        {
            // TODO: Add data validation
            tag.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemTags.Update(tag);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemTag tag = new ItemTag();
            tag.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemTags.Remove(tag);
                dataContext.SaveChanges();
            }
        }
    }
}
