using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                dataFields = dataContext.ItemFields
                    .Include(field => field.Category)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .ToList();

                return dataFields;
            }
        }

        // GET api/itemfields/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemField> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemFieldItem = dataContext.ItemFields
                    .Include(field => field.Category)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .SingleOrDefault(x => x.ID == id);

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
                int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);

                itemField.Category = dataContext.Categories.Find(categoryQuery);
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
                int categoryQuery = Convert.ToInt32(HttpContext.Request.Query["Category"]);

                itemField.Category = dataContext.Categories.Find(categoryQuery);
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
