using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;
using OryxWestAfrica.Models.ViewModels;

namespace OryxWestAfrica.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OryxAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OryxAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var picture =new Picture();
            PopulatePix(picture);
           
            return View();
        }

        public IActionResult Imagechecked(string[] selectedImages)
        {
            var pi = new Picture();
           
            var imagetoUpdate = _context.Pictures.OrderByDescending(m=>m.PictureID).FirstOrDefault();
            UpdateInCheckedImage(selectedImages, imagetoUpdate);

            return RedirectToAction(nameof(Index));
           
        }

    
        private void PopulatePix(Picture picture )
        {
            
            var pictures = _context.Pictures;
           
           
            var viewModel = new List<CheckUpdate>();
            foreach (var pix in pictures)
            {
                viewModel.Add(new CheckUpdate
                {
                    CheckUpdateID = pix.PictureID,
                    PictureName = pix.PictureName,
                   Assigned = Convert.ToBoolean(pix.Chcker)
                });
            }
            ViewData["Images"] = viewModel;
        }


        private void UpdateInCheckedImage(string[] selectedImages, Picture pictureTocheck)
        {
            if (selectedImages == null)
            {
                pictureTocheck.CheckedImages = new List<CheckedImage>();
                return;
            }
            //.CheckedImages.Select
            var selectedImageHS = new HashSet<string>(selectedImages);
            var SliderImage = new HashSet<int>(pictureTocheck.CheckedImages.Select(c => c.Picture.PictureID));

            foreach (var image in _context.Pictures)
            {
                if (selectedImageHS.Contains(image.PictureID.ToString()))
                {
                    if (!SliderImage.Contains(image.PictureID))
                    {
                        pictureTocheck.CheckedImages.Add(new CheckedImage { CheckedImageID = image.PictureID });

                    }
                }
                else
                {

                    if (SliderImage.Contains(pictureTocheck.PictureID))
                    {
                        CheckedImage imageToRemove = pictureTocheck.CheckedImages.SingleOrDefault(i => i.CheckedImageID == pictureTocheck.PictureID);
                        _context.Remove(imageToRemove);
                    }
                }
            }
        }

      

    }
}