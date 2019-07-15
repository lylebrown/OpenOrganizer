using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly APIDBContext db;
        public CategoriesController(APIDBContext context)
        {
            db = context;
        }
        // GET api/categories
        [HttpGet]
        public IActionResult Get()
        {
            int limitQuery = Convert.ToInt32(HttpContext.Request.Query["Limit"]);
            int startingAfterQuery = Convert.ToInt32(HttpContext.Request.Query["Starting_After"]);
            // TODO: Add advanced search later, primitive search on all controllers like this for now.
            string searchQuery = HttpContext.Request.Query["Search"].ToString().ToLower();

            var searchCategoryIds = db.Categories.Where(c => c.Name.ToLower().Contains(searchQuery)).Select(c => c.ID);

            var results = db.Categories
                    .Include(cat => cat.Parent)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .Where(result =>
                        searchCategoryIds.Contains(result.Parent.ID)
                        || result.Name.ToLower().Contains(searchQuery));

            if (startingAfterQuery > 0)
            {
                results = results.Skip(startingAfterQuery);
            }
            if (limitQuery > 0)
            {
                results = results.Take(limitQuery);
            }


            return Ok(results);
        }


        // GET api/categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var categoryItem = db.Categories
                    .Include(cat => cat.Parent)
                        .ThenInclude(cat => cat.Parent)
                            .ThenInclude(cat => cat.Parent)
                    .SingleOrDefault(x => x.ID == id);

            if (categoryItem == null)
            {
                return NotFound();
            }

            return Ok(categoryItem);
        }

        // POST api/categories
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            int parentQuery = Convert.ToInt32(HttpContext.Request.Query["Parent"]);
            // TODO: Add data validation
            category.Parent = db.Categories.Find(parentQuery);

            db.Categories.Add(category);
            db.SaveChanges();
            return Ok(category.ID);
        }

        /* DEPRECATED
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
        */

        // PUT api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            int parentQuery = Convert.ToInt32(HttpContext.Request.Query["Parent"]);
            // TODO: Add data validation
            category.Parent = db.Categories.Find(parentQuery);

            category.ID = id;
            db.Categories.Update(category);
            db.SaveChanges();
            return Ok(category.ID);
        }

        // DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = new Category();
            category.ID = id;
            db.Categories.Remove(category);
            db.SaveChanges();
            return Ok();
        }
    }
}
