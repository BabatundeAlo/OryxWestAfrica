using OryxWestAfrica.Models.ViewModels;
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
        [Display(Name = "Name")]
        [Required]
        public string PictureName { get; set; }
        [Display(Name = "Upload")]
        [Required]
        public byte[] Image { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Tag")]
        [Required]
        public string Differentiator { get; set; }
        [Display(Name = "Set As Banner")]
        [Required]
        public bool Chcker { get; set; }
        public ICollection<CheckedImage> CheckedImages { get; set; }
    }
}
