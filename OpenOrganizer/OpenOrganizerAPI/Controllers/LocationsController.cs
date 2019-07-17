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
    public class LocationsController : ControllerBase
    {
        private readonly APIDBContext db;
        public LocationsController(APIDBContext context)
        {
            db = context;
        }
        // GET api/locations
        [HttpGet]
        public IActionResult Get()
        {
            int limitQuery = Convert.ToInt32(HttpContext.Request.Query["Limit"]);
            int startingAfterQuery = Convert.ToInt32(HttpContext.Request.Query["Starting_After"]);
            // TODO: Add advanced search later, primitive search on all controllers like this for now.
            string searchQuery = HttpContext.Request.Query["Search"].ToString().ToLower();

            var searchLocationIds = db.Locations.Where(c => c.Name.ToLower().Contains(searchQuery)).Select(c => c.ID);

            var results = db.Locations
                    .Include(loc => loc.Parent)
                        .ThenInclude(loc => loc.Parent)
                    .Where(result =>
                        searchLocationIds.Contains(result.Parent.ID)
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

        // GET api/locations/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var itemLocation = db.Locations
                    .Include(loc => loc.Parent)
                        .ThenInclude(loc => loc.Parent)
                    .SingleOrDefault(x => x.ID == id);

            if (itemLocation == null)
            {
                return NotFound();
            }

            return Ok(itemLocation);
        }

        // POST api/locations
        [HttpPost]
        public IActionResult Post([FromBody] Location location)
        {
            // TODO: Add data validation
            int parentQuery = Convert.ToInt32(HttpContext.Request.Query["Parent"]);

            location.Parent = db.Locations.Find(parentQuery);
            db.Locations.Add(location);
            db.SaveChanges();
            return Ok(location.ID);
        }

        // POST api/locations/childof/{id}
        // Allows for child locations to be created with the specified parent ID
        /* DEPRECATED
         * [Route("childof/{id}")]
        [HttpPost("{id}")]
        public void PostChild(int id, [FromBody] Location location)
        {
            location.Parent = db.Locations.Find(id);
            db.Locations.Add(location);
            db.SaveChanges();
        } */

        // PUT api/locations/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Location location)
        {
            // TODO: Add data validation
            int parentQuery = Convert.ToInt32(HttpContext.Request.Query["Parent"]);

            location.ID = id;
            location.Parent = db.Locations.Find(parentQuery);
            db.Locations.Update(location);
            db.SaveChanges();
            return Ok(location.ID);
        }

        // DELETE api/locations/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Location location = new Location();
            location.ID = id;
            db.Locations.Remove(location);
            db.SaveChanges();
            return Ok();
        }
    }
}
