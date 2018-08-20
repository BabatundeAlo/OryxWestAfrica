using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;
using OryxWestAfrica.Models.ViewModels;

namespace OryxWestAfrica.Controllers
{
    public class PicturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pictures
        public async Task<IActionResult> Index()
        {
            var pixlist = _context.Pictures;
            ViewData["Mee"] = pixlist.ToList();
            return View(await _context.Pictures.ToListAsync());
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.PictureID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PictureID,PictureName,Image,Description,Differentiator")] Picture picture, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    foreach (var item in Image)
                    {
                        if (item.Length > 0)
                        {

                            using (var stream = new MemoryStream())


                            {
                                await item.CopyToAsync(stream);
                                picture.Image = stream.ToArray();

                            }
                        }
                        else

                        {

                            picture.Image = picture.Image;
                        }
                    }

                    _context.Add(picture);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.PictureID))
                    {
                        return NotFound();
                    }
                }
            }
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PictureID,PictureName,Image,Description,Differentiator,Chcker")] Picture picture, List<IFormFile> Image)
        {
            if (id != picture.PictureID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in Image)
                    {
                        if (item.Length > 0)
                        {

                            using (var stream = new MemoryStream())


                            {
                                await item.CopyToAsync(stream);
                                picture.Image = stream.ToArray();
                                
                                
                            }
                        }
                        else

                        {

                            picture.Image = picture.Image;
                        }
                    }
                    _context.Update(picture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.PictureID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.PictureID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }
        private void PopulatePix(Picture picture)
        {
            //Picture picture
            var pictures = _context.Pictures;

            var viewModel = new List<SliderView>();
            foreach (var pix in pictures)
            {
                viewModel.Add(new SliderView
                {
                    PictureID = pix.PictureID,
                    PictureName = pix.PictureName,
                    Assigned = Convert.ToBoolean(pix.Chcker)
                });
            }
            ViewData["Images"] = viewModel;
        }

        [HttpGet]
        public IActionResult Checker()
        {
            var listr = _context.Pictures.ToList();
          
            return View(listr);
        }

        [HttpPost]
        public IActionResult Checker(Picture picture)
        {
            var listr = _context.Pictures.ToList();

            return View(listr);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PictureExists(int id)
        {
            return _context.Pictures.Any(e => e.PictureID == id);
        }
    }
}
