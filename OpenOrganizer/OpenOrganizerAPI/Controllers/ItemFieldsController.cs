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
    public class ItemFieldsController : ControllerBase
    {
        // GET api/itemfields
        [HttpGet]
        public ActionResult<List<ItemField>> Get()
        {
            List<ItemField> dataFields = new List<ItemField>();
            using (var dataContext = new APIDBContext())
            {
                dataFields = dataContext.ItemFields.AsQueryable().ToList();

                return dataFields;
            }
        }

        // GET api/itemfields/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemField> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemFieldItem = dataContext.ItemFields.Find(id);

                if (itemFieldItem == null)
                {
                    return NotFound();
                }

                return itemFieldItem;
            }
        }

        // POST api/itemfields
        [HttpPost]
        public void Post([FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFields.Add(itemField);
                dataContext.SaveChanges();
            }
        }

        // PUT api/itemfields/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemField itemField)
        {
            // TODO: Add data validation
            itemField.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFields.Update(itemField);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/itemfields/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemField itemField = new ItemField();
            itemField.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.ItemFields.Remove(itemField);
                dataContext.SaveChanges();
            }
        }
    }
}
