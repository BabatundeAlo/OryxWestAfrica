using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OryxWestAfrica.Models
{
    public class CheckedImage
    {
       
        public int CheckedImageID { get; set; }
        public Picture Picture { get; set; }
    }
}
