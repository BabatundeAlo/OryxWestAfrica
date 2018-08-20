using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DemoTestersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoTestersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DemoTesters
        public async Task<IActionResult> Index()
        {
            return View(await _context.DemoTesters.ToListAsync());
        }

        // GET: DemoTesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoTester = await _context.DemoTesters
                .FirstOrDefaultAsync(m => m.DemoTesterID == id);
            if (demoTester == null)
            {
                return NotFound();
            }

            return View(demoTester);
        }

        [Authorize(Roles = "Admin")]
        // GET: DemoTesters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DemoTesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DemoTesterID,DemoName,Image,Description,Differentiator")] DemoTester demoTester)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demoTester);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(demoTester);
        }

        // GET: DemoTesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoTester = await _context.DemoTesters.FindAsync(id);
            if (demoTester == null)
            {
                return NotFound();
            }
            return View(demoTester);
        }

        // POST: DemoTesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DemoTesterID,DemoName,Image,Description,Differentiator")] DemoTester demoTester)
        {
            if (id != demoTester.DemoTesterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demoTester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemoTesterExists(demoTester.DemoTesterID))
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
            return View(demoTester);
        }

        // GET: DemoTesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demoTester = await _context.DemoTesters
                .FirstOrDefaultAsync(m => m.DemoTesterID == id);
            if (demoTester == null)
            {
                return NotFound();
            }

            return View(demoTester);
        }

        // POST: DemoTesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demoTester = await _context.DemoTesters.FindAsync(id);
            _context.DemoTesters.Remove(demoTester);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemoTesterExists(int id)
        {
            return _context.DemoTesters.Any(e => e.DemoTesterID == id);
        }
    }
}
