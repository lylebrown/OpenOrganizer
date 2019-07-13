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
    public class CategoriesController : ControllerBase
    {
        private readonly APIDBContext db = new APIDBContext();
        // GET api/categories
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return db.Categories.ToList();
        }

        // GET api/categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var categoryItem = db.Categories.Find(id);
            return categoryItem == null ? NotFound() : new ActionResult<Category>(categoryItem);
        }

        // POST api/categories
        [HttpPost]
        public void Post([FromBody] Category category)
        {
            // TODO: Add data validation
            db.Categories.Add(category);
            db.SaveChanges();
        }

        // POST api/categories/childof/{id}
        // Allows for child categories to be created with the specified parent ID
        [Route("childof/{id}")]
        [HttpPost("{id}")]
        public void PostChild(int id, [FromBody] Category category)
        {
            category.Parent = db.Categories.Find(id);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        // PUT api/categories/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category category)
        {
            // TODO: Add data validation
            category.ID = id;
            db.Categories.Update(category);
            db.SaveChanges();
        }

        // DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Category category = new Category();
            category.ID = id;
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
