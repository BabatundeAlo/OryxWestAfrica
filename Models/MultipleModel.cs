

using Microsoft.AspNetCore.Http.Internal;
using System.Collections.Generic;

namespace OryxWestAfrica.Models
{
    public class MultipleModel
    {
        public Gallery gallery { get; set; }
       public GaleryImage GaleryImage { get; set; }
      //  public List<FormFile> Files { get; set; }
    }
}
