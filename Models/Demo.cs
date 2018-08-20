using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Demo
    {
        [Key]
        public int DemoTesterID { get; set; }
        public string DemoName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Differentiator { get; set; }
        [NotMapped]
        [Display(Name = "Roles")]
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        [Key]
        public String RoleId { get; set; }
        public String RoleName { get; set; }

        public bool Selected { get; set; }
    }
}

