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
    public string ClientName { get; set; }
    public string ClientDesc { get; set; }
    public byte[] Image { get; set; }

}
}
