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
    public class TagsController : ControllerBase
    {
        // GET api/tags
        [HttpGet]
        public ActionResult<List<Tag>> Get()
        {
            List<Tag> dataTags = new List<Tag>();
            using (var dataContext = new APIDBContext())
            {
                dataTags = dataContext.Tags.AsQueryable().ToList();

                return dataTags;
            }
        }

        // GET api/tags/{id}
        [HttpGet("{id}")]
        public ActionResult<Tag> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var tagsItem = dataContext.Tags.Find(id);

                if (tagsItem == null)
                {
                    return NotFound();
                }

                return tagsItem;
            }
        }

        // POST api/tags
        [HttpPost]
        public void Post([FromBody] Tag tag)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.Tags.Add(tag);
                dataContext.SaveChanges();
            }
        }

        // PUT api/tags/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Tag tag)
        {
            // TODO: Add data validation
            tag.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Tags.Update(tag);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/tags/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Tag tag = new Tag();
            tag.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Tags.Remove(tag);
                dataContext.SaveChanges();
            }
        }
    }
}
