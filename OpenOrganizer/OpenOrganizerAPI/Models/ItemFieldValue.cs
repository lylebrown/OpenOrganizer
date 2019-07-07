using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class ItemFieldValue
    {
        public int ID { get; set; }
        public int ItemId { get; set; }
        public int FieldId { get; set; }
        public string Value { get; set; }
    }
}
