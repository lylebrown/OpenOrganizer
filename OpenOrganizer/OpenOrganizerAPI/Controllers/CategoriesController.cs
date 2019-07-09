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
        // GET api/categories
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            List<Category> dataCategories = new List<Category>();
            using (var dataContext = new APIDBContext())
            {
                dataCategories = dataContext.Categories.AsQueryable().ToList();

                return dataCategories;
            }
        }

        // GET api/categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var categoryItem = dataContext.Categories.Find(id);

                if (categoryItem == null)
                {
                    return NotFound();
                }

                return categoryItem;
            }
        }

        // POST api/categories
        [HttpPost]
        public void Post([FromBody] Category category)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.Categories.Add(category);
                dataContext.SaveChanges();
            }
        }

        // POST api/categories/childof/{id}
        // Allows for child categories to be created with the specified parent ID
        [Route("childof/{id}")]
        [HttpPost("{id}")]
        public void PostChild(int id, [FromBody] Category category)
        {
            using (var dataContext = new APIDBContext())
            {
                category.Parent = dataContext.Categories.Find(id);
                dataContext.Categories.Add(category);
                dataContext.SaveChanges();
            }
        }

        // PUT api/categories/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category category)
        {
            // TODO: Add data validation
            category.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Categories.Update(category);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Category category = new Category();
            category.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Categories.Remove(category);
                dataContext.SaveChanges();
            }
        }
    }
}
