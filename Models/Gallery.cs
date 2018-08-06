using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string GalleryName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Differentiator { get; set; }
    }
}
