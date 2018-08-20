using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Client
    { 
        [Key]
     public int ClientId { get; set; }
        [Display(Name = "Client Name")]
        [Required]
    public string ClientName { get; set; }
        [Display(Name = "Client Description")]
        [Required]
        public string ClientDesc { get; set; }
        [Display(Name = "Upload Image")]
        [Required]
        public byte[] Image { get; set; }

}
}
