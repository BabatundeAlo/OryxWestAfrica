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
        public async Task<IActionResult> Index()
        {

            var picture =new Picture();
            PopulatePix(picture);
            //(picture);
            return View();
        }



        private void PopulatePix(Picture picture )
        {
            //Picture picture
            var pictures = _context.Pictures;
            
            var viewModel = new List<SliderView>();
            foreach (var pix in pictures)
            {
                viewModel.Add(new SliderView
                {
                    PictureID = pix.PictureID ,
                    PictureName = pix.PictureName,
                   // Assigned =Convert.ToBoolean( pix.Differentiator)
                });
            }
            ViewData["Courses"] = viewModel;
        }
    }
}