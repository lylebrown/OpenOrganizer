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
    }
}
