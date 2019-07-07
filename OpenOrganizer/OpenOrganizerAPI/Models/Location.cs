using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public Location Parent { get; set; }
    }
}
