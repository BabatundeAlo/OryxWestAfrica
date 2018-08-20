﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Controllers
{


    [Authorize(Roles = "Admin")]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,ClientDesc,Image")] Client client, List<IFormFile> Image)
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
                                client.Image = stream.ToArray();

                            }
                        }
                        else

                        {

                            client.Image = client.Image;
                        }
                    }

                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                }
            }
                return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,ClientDesc,Image")] Client client, List<IFormFile> Image)
        {
            if (id != client.ClientId)
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
                                client.Image = stream.ToArray();

                            }
                        }
                        else

                        {

                            client.Image = client.Image;
                        }
                    }
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        //public async Task<IActionResult> MultipleUpload(MultipleModel md, IList<IFormFile> files)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _context.Add(md.gallery);
        //        _context.SaveChanges();

        //        foreach (var file in files)
        //        {
        //            GaleryImage img = new GaleryImage();

        //            if (file.Length > 0)
        //            {
        //                using (var stream = new MemoryStream())
        //                {
        //                    await file.CopyToAsync(stream);

        //                    img.Image = stream.ToArray();
        //                    img.Tag = string.Empty;
        //                    img.GalleryID = md.gallery.GalleryID;
        //                }

        //            }
        //            _context.GaleryImages.Add(img);
        //        }
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(MultipleUpload));
        //    }
        //    return View();
        //}

    }
}
