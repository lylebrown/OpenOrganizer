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
        // GET api/itemattachments
        [HttpGet]
        public ActionResult<List<ItemAttachment>> Get()
        {
            List<ItemAttachment> dataAttachments = new List<ItemAttachment>();
            using (var dataContext = new APIDBContext())
            {
                dataAttachments = dataContext.ItemAttachments.AsQueryable().ToList();

                return dataAttachments;
            }
        }

        // GET api/itemattachments/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemAttachment> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var attachmentItem = dataContext.ItemAttachments.Find(id);

                if (attachmentItem == null)
                {
                    return NotFound();
                }

                return attachmentItem;
            }
        }

        // POST api/itemattachments
        [HttpPost]
        public void Post([FromBody] ItemAttachment attachment)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemAttachments.Add(attachment);
                dataContext.SaveChanges();
            }
        }

        // PUT api/itemattachments/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemAttachment attachment)
        {
            // TODO: Add data validation
            attachment.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemAttachments.Update(attachment);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/itemattachments/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemAttachment attachment = new ItemAttachment();
            attachment.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemAttachments.Remove(attachment);
                dataContext.SaveChanges();
            }
        }
    }
}
