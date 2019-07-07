using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class ItemTag
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int TagID { get; set; }
    }
}
