using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class ItemFieldValue
    {
        public int ItemId { get; set; }
        public int FieldId { get; set; }
        public string Value { get; set; }
    }
}
