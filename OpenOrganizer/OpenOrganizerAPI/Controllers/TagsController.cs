using Microsoft.AspNetCore.Mvc;
using OpenOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Controllers
{
    [Route("api/v1/[controller]")]
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
        public IActionResult Get()
        {
            List<Tag> dataTags = new List<Tag>();
            dataTags = db.Tags.ToList();
            return Ok(dataTags);
        }

        // GET api/tags/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tagsItem = db.Tags.Find(id);

            if (tagsItem == null)
            {
                return NotFound();
            }

            return Ok(tagsItem);
        }

        // POST api/tags
        [HttpPost]
        public IActionResult Post([FromBody] Tag tag)
        {
            // TODO: Add data validation
            db.Tags.Add(tag);
            db.SaveChanges();
            return Ok(tag.ID);
        }

        // PUT api/tags/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tag tag)
        {
            // TODO: Add data validation
            tag.ID = id;
            db.Tags.Update(tag);
            db.SaveChanges();
            return Ok(tag.ID);
        }

        // DELETE api/tags/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Tag tag = new Tag();
            tag.ID = id;
            db.Tags.Remove(tag);
            db.SaveChanges();
            return Ok();
        }
    }
}
