using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Picture
    {
        [Key]
        public int PictureID { get; set; }
        public string PictureName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Differentiator { get; set; }
    }
}
