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
    public class LocationsController : ControllerBase
    {
        // GET api/locations
        [HttpGet]
        public ActionResult<List<Location>> Get()
        {
            List<Location> dataLocations = new List<Location>();
            using (var dataContext = new APIDBContext())
            {
                dataLocations = dataContext.Locations.AsQueryable().ToList();

                return dataLocations;
            }
        }

        // GET api/locations/{id}
        [HttpGet("{id}")]
        public ActionResult<Location> Get(int id)
        {
            using (var dataContext = new APIDBContext())
            {
                var itemLocation = dataContext.Locations.Find(id);

                if (itemLocation == null)
                {
                    return NotFound();
                }

                return itemLocation;
            }
        }

        // POST api/locations
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            // TODO: Add data validation
            using (var dataContext = new APIDBContext())
            {
                dataContext.Locations.Add(location);
                dataContext.SaveChanges();
            }
        }

        // POST api/locations/childof/{id}
        // Allows for child locations to be created with the specified parent ID
        [Route("childof/{id}")]
        [HttpPost("{id}")]
        public void PostChild(int id, [FromBody] Location location)
        {
            using (var dataContext = new APIDBContext())
            {
                location.Parent = dataContext.Locations.Find(id);
                dataContext.Locations.Add(location);
                dataContext.SaveChanges();
            }
        }

        // PUT api/locations/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Location location)
        {
            // TODO: Add data validation
            location.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Locations.Update(location);
                dataContext.SaveChanges();
            }
        }

        // DELETE api/locations/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Location location = new Location();
            location.ID = id;
            using (var dataContext = new APIDBContext())
            {
                dataContext.Locations.Remove(location);
                dataContext.SaveChanges();
            }
        }
    }
}
