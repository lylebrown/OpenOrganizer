using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class ItemFieldValue
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public int ItemFieldID { get; set; }
        public ItemField ItemField { get; set; }
        public string Value { get; set; }
    }
}
