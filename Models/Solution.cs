using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Solution
    {
        [Key]
        public int SolutionId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public byte[] Image { get; set; }
        public string Link { get; set; }
    }
}
