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
        private readonly APIDBContext db;
        public TagsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/tags
        [HttpGet]
        public ActionResult<List<Tag>> Get()
        {
            List<Tag> dataTags = new List<Tag>();
            dataTags = db.Tags.ToList();
            return dataTags;
        }

        // GET api/tags/{id}
        [HttpGet("{id}")]
        public ActionResult<Tag> Get(int id)
        {
            var tagsItem = db.Tags.Find(id);

            if (tagsItem == null)
            {
                return NotFound();
            }

            return tagsItem;
        }

        // POST api/tags
        [HttpPost]
        public void Post([FromBody] Tag tag)
        {
            // TODO: Add data validation
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        // PUT api/tags/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Tag tag)
        {
            // TODO: Add data validation
            tag.ID = id;
            db.Tags.Update(tag);
            db.SaveChanges();
        }

        // DELETE api/tags/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Tag tag = new Tag();
            tag.ID = id;
            db.Tags.Remove(tag);
            db.SaveChanges();
        }
    }
}
