using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIN_Projekt.Data;
using PIN_Projekt.Models;

namespace PIN_Projekt.Controllers
{
    [Authorize]
    public class SmjersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SmjersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Smjers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Smjers.ToListAsync());
        }

        // GET: Smjers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smjer = await _context.Smjers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smjer == null)
            {
                return NotFound();
            }

            return View(smjer);
        }

        // GET: Smjers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Smjers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Smjer smjer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(smjer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(smjer);
        }

        // GET: Smjers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smjer = await _context.Smjers.FindAsync(id);
            if (smjer == null)
            {
                return NotFound();
            }
            return View(smjer);
        }

        // POST: Smjers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Smjer smjer)
        {
            if (id != smjer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smjer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmjerExists(smjer.Id))
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
            return View(smjer);
        }

        // GET: Smjers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smjer = await _context.Smjers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smjer == null)
            {
                return NotFound();
            }

            return View(smjer);
        }

        // POST: Smjers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var smjer = await _context.Smjers.FindAsync(id);
            if (smjer != null)
            {
                _context.Smjers.Remove(smjer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmjerExists(int id)
        {
            return _context.Smjers.Any(e => e.Id == id);
        }
    }
}
