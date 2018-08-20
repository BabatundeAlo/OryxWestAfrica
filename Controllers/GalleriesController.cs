using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingenvironment;
        public GalleriesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingenvironment = hostingEnvironment;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
          
            return View(await _context.Galleries.ToListAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryID,GalleryName,Image,Description,Differentiator")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryID,GalleryName,Image,Description,Differentiator")] Gallery gallery)
        {
            if (id != gallery.GalleryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.GalleryID))
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
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            return _context.Galleries.Any(e => e.GalleryID == id);
        }



        public IActionResult Gallery()
        {
            //ViewBag.Gallery = new SelectList(_context.Galleries, "GalleryID", "GalleryID");
            ViewBag.Gallery = _context.Galleries.Select(m => m.GalleryID).ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Gallery(IList<IFormFile> files)
        {
            foreach (IFormFile item in files)
            {

                string filename = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureFilenme(filename);
                using (FileStream filestream = System.IO.File.Create(this.Getpath(filename)))
                {
                     

                }
            }
            return this.Content("Success");
        }

        private string Getpath(string filename)
        {
            //throw new NotImplementedException();
            string path = _hostingenvironment.WebRootPath + "\\upload\\";
            if (!Directory.Exists(path))

                Directory.CreateDirectory(path);
            return path + filename;

        }

        private string EnsureFilenme(string filename)
        {
            // throw new NotImplementedException();
            if (filename.Contains("\\"))

                filename = filename.Substring(filename.LastIndexOf("\\") + 1);


            return filename;
        }








        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public IActionResult Display()
        {
            string path= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
           
            var item = Directory.GetFiles(path).ToList();
            
            return View(item);
        }


        public IActionResult MultipleUpload()
        {
            ViewBag.Gallery = new SelectList(_context.Galleries, "GalleryID", "GalleryID");
          //  ViewBag.Gallery = _context.Galleries.Select(m => m.GalleryID).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MultipleUpload(MultipleModel md, IList<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
               _context.Add(md.gallery);
                _context.SaveChanges();

                foreach (var file in files)
                {
                    GaleryImage img = new GaleryImage();
                    
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                           
                            img.Image = stream.ToArray();
                            img.Tag = string.Empty;
                            img.GalleryID = md.gallery.GalleryID;
                        }

                    }
                    _context.GaleryImages.Add(img);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MultipleUpload));
            }
           return View();
        }

       
    }
}
