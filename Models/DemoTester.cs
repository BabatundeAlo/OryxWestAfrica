using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class DemoTester
    {
        [Key]
        public int DemoTesterID { get; set; }
        public string DemoName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Differentiator { get; set; }
    }
}
