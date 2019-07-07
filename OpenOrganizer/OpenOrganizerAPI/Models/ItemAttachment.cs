using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI.Models
{
    public class ItemAttachment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] File { get; set; }
    }
}
