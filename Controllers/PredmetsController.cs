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
    public class PredmetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredmetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Predmets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Predmets.Include(p => p.SmjerParent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Predmets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predmet = await _context.Predmets
                .Include(p => p.SmjerParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predmet == null)
            {
                return NotFound();
            }

            return View(predmet);
        }

        // GET: Predmets/Create
        public IActionResult Create()
        {
            ViewData["SmjerId"] = new SelectList(_context.Smjers, "Id", "Id");
            return View();
        }

        // POST: Predmets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,SmjerId")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predmet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SmjerId"] = new SelectList(_context.Smjers, "Id", "Id", predmet.SmjerId);
            return View(predmet);
        }

        // GET: Predmets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predmet = await _context.Predmets.FindAsync(id);
            if (predmet == null)
            {
                return NotFound();
            }
            ViewData["SmjerId"] = new SelectList(_context.Smjers, "Id", "Id", predmet.SmjerId);
            return View(predmet);
        }

        // POST: Predmets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,SmjerId")] Predmet predmet)
        {
            if (id != predmet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predmet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredmetExists(predmet.Id))
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
            ViewData["SmjerId"] = new SelectList(_context.Smjers, "Id", "Id", predmet.SmjerId);
            return View(predmet);
        }

        // GET: Predmets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predmet = await _context.Predmets
                .Include(p => p.SmjerParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predmet == null)
            {
                return NotFound();
            }

            return View(predmet);
        }

        // POST: Predmets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predmet = await _context.Predmets.FindAsync(id);
            if (predmet != null)
            {
                _context.Predmets.Remove(predmet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredmetExists(int id)
        {
            return _context.Predmets.Any(e => e.Id == id);
        }
    }
}
