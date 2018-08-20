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
        [Display(Name = "Solution")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Details { get; set; }
        [Display(Name = "Upload Image")]
        [Required]
        public byte[] Image { get; set; }
        public string Link { get; set; }
    }
}
