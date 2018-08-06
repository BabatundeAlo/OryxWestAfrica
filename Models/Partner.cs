using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Partner
    {
        public int PartnerID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Details { get; set; }
    }
}
