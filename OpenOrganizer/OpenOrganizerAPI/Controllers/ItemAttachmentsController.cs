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
    public class ItemAttachmentsController : ControllerBase
    {
        private readonly APIDBContext db;
        public ItemAttachmentsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/itemattachments
        [HttpGet]
        public ActionResult<List<ItemAttachment>> Get()
        {
            return db.ItemAttachments.ToList();
        }

        // GET api/itemattachments/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemAttachment> Get(int id)
        {
            var attachmentItem = db.ItemAttachments.Find(id);

            if (attachmentItem == null)
            {
                return NotFound();
            }

            return attachmentItem;
        }

        // POST api/itemattachments
        [HttpPost]
        public void Post([FromBody] ItemAttachment attachment)
        {
            // TODO: Add data validation
            db.ItemAttachments.Add(attachment);
            db.SaveChanges();
        }

        // PUT api/itemattachments/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemAttachment attachment)
        {
            // TODO: Add data validation
            attachment.ID = id;
            db.ItemAttachments.Update(attachment);
            db.SaveChanges();
        }

        // DELETE api/itemattachments/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemAttachment attachment = new ItemAttachment();
            attachment.ID = id;
            db.ItemAttachments.Remove(attachment);
            db.SaveChanges();
        }
    }
}
