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
    public class IspitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IspitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ispits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ispits.Include(i => i.PredmetParent).Include(i => i.StudentParent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ispits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ispit = await _context.Ispits
                .Include(i => i.PredmetParent)
                .Include(i => i.StudentParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ispit == null)
            {
                return NotFound();
            }

            return View(ispit);
        }

        // GET: Ispits/Create
        public IActionResult Create()
        {
            ViewData["PredmetId"] = new SelectList(_context.Predmets, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Ispits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocijena,BrojBodova,DatumPolaganja,StudentId,PredmetId")] Ispit ispit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ispit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PredmetId"] = new SelectList(_context.Predmets, "Id", "Id", ispit.PredmetId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", ispit.StudentId);
            return View(ispit);
        }

        // GET: Ispits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ispit = await _context.Ispits.FindAsync(id);
            if (ispit == null)
            {
                return NotFound();
            }
            ViewData["PredmetId"] = new SelectList(_context.Predmets, "Id", "Id", ispit.PredmetId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", ispit.StudentId);
            return View(ispit);
        }

        // POST: Ispits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ocijena,BrojBodova,DatumPolaganja,StudentId,PredmetId")] Ispit ispit)
        {
            if (id != ispit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ispit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IspitExists(ispit.Id))
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
            ViewData["PredmetId"] = new SelectList(_context.Predmets, "Id", "Id", ispit.PredmetId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", ispit.StudentId);
            return View(ispit);
        }

        // GET: Ispits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ispit = await _context.Ispits
                .Include(i => i.PredmetParent)
                .Include(i => i.StudentParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ispit == null)
            {
                return NotFound();
            }

            return View(ispit);
        }

        // POST: Ispits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ispit = await _context.Ispits.FindAsync(id);
            if (ispit != null)
            {
                _context.Ispits.Remove(ispit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IspitExists(int id)
        {
            return _context.Ispits.Any(e => e.Id == id);
        }
    }
}
