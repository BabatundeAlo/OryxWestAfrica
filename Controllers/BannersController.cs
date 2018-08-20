using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Controllers
{
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingenvironment;

        public BannersController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingenvironment = hostingEnvironment;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banners.ToListAsync());
        }

        // GET: Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Banners/Create
        public IActionResult Create()
        {
            return View();
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
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerID,Name,Url,Tag,Description")] Banner banner, IList<IFormFile> file)
        {
            //string ImageName;
          
                if (ModelState.IsValid)
                {            
                  for (int i =0; i< file.Count; i++)
                    {


                        string filename = ContentDispositionHeaderValue.Parse(file[i].ContentDisposition).FileName.Trim('"');
                        using (FileStream filestream = System.IO.File.Create(Getpath(filename)))
                        {
                          
                        }
                    string ImageName = Path.GetFileName(file[i].FileName);

                    var filepath = Path.GetFullPath(ImageName);
                    banner.Name = ImageName;
                    banner.Url = filepath;
                    _context.Banners.Add(banner);
                        await _context.SaveChangesAsync();
                    
                  }
               
              
                return RedirectToAction(nameof(Index));
                }
            return View(banner);
        }

        public IActionResult GetImage()
        {
           var model =_context.Banners.ToList();
           return View(model);
        }


        // GET: Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerID,Name,Url,Tag,Description")] Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id))
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
            return View(banner);
        }

        // GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.Id == id);
        }


    }
}
