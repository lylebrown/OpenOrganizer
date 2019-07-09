using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int CategoryID { get; set; }
        public Category Category { get; set; }
        //public int LocationID { get; set; }
        public Location Location { get; set; }
    }
}
