using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        //public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public Location Location { get; set; }
        // TODO: Add attachment handling
        //public List<ItemAttachment> Attachments { get; set; }
    }
}
