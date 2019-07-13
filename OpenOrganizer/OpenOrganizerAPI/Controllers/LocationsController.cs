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
        private readonly APIDBContext db = new APIDBContext();
        // GET api/locations
        [HttpGet]
        public ActionResult<List<Location>> Get()
        {
            List<Location> dataLocations = new List<Location>();
            dataLocations = db.Locations.ToList();

            return dataLocations;
        }

        // GET api/locations/{id}
        [HttpGet("{id}")]
        public ActionResult<Location> Get(int id)
        {
            var itemLocation = db.Locations.Find(id);

            if (itemLocation == null)
            {
                return NotFound();
            }

            return itemLocation;
        }

        // POST api/locations
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }

        // POST api/locations/childof/{id}
        // Allows for child locations to be created with the specified parent ID
        [Route("childof/{id}")]
        [HttpPost("{id}")]
        public void PostChild(int id, [FromBody] Location location)
        {
            location.Parent = db.Locations.Find(id);
            db.Locations.Add(location);
            db.SaveChanges();
        }

        // PUT api/locations/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Location location)
        {
            // TODO: Add data validation
            location.ID = id;
            db.Locations.Update(location);
            db.SaveChanges();
        }

        // DELETE api/locations/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Location location = new Location();
            location.ID = id;
            db.Locations.Remove(location);
            db.SaveChanges();
        }
    }
}
