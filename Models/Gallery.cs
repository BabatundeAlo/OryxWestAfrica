using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class Gallery
    {

        public Gallery()
        {
            Images = new HashSet<GaleryImage>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GalleryID { get; set; }
        public string GalleryName { get; set; }
            
        public string Description { get; set; }
        public string Differentiator { get; set; }
        public HashSet<GaleryImage> Images { get; set; }
    }
   
    public class GaleryImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GalleryID { get; set; }
        public string Tag { get; set; }
        public byte[] Image { get; set; }
    }
}
