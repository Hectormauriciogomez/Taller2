using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMecanicoC.Models;

namespace TMecanicoC.Controllers
{
    public class RevNivelesController : Controller
    {
        private readonly DBTMecanicoCContext _context;

        public RevNivelesController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        // GET: RevNiveles
        public async Task<IActionResult> Index()
        {
            var dBTMecanicoCContext = _context.RevNiveles.Include(r => r.PlacaNavigation);
            return View(await dBTMecanicoCContext.ToListAsync());
        }

        // GET: RevNiveles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RevNiveles == null)
            {
                return NotFound();
            }

            var revNiveles = await _context.RevNiveles
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdRevNiveles == id);
            if (revNiveles == null)
            {
                return NotFound();
            }

            return View(revNiveles);
        }

        // GET: RevNiveles/Create
        public IActionResult Create()
        {
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa");
            return View();
        }

        // POST: RevNiveles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRevNiveles,FechaHora,NivelAceite,NivelFrenos,NivelRefrigerante,NivelDireccion,Placa")] RevNiveles revNiveles)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(revNiveles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", revNiveles.Placa);
            return View(revNiveles);
        }

        // GET: RevNiveles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RevNiveles == null)
            {
                return NotFound();
            }

            var revNiveles = await _context.RevNiveles.FindAsync(id);
            if (revNiveles == null)
            {
                return NotFound();
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", revNiveles.Placa);
            return View(revNiveles);
        }

        // POST: RevNiveles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRevNiveles,FechaHora,NivelAceite,NivelFrenos,NivelRefrigerante,NivelDireccion,Placa")] RevNiveles revNiveles)
        {
            if (id != revNiveles.IdRevNiveles)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(revNiveles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevNivelesExists(revNiveles.IdRevNiveles))
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
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", revNiveles.Placa);
            return View(revNiveles);
        }

        // GET: RevNiveles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RevNiveles == null)
            {
                return NotFound();
            }

            var revNiveles = await _context.RevNiveles
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdRevNiveles == id);
            if (revNiveles == null)
            {
                return NotFound();
            }

            return View(revNiveles);
        }

        // POST: RevNiveles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RevNiveles == null)
            {
                return Problem("Entity set 'DBTMecanicoCContext.RevNiveles'  is null.");
            }
            var revNiveles = await _context.RevNiveles.FindAsync(id);
            if (revNiveles != null)
            {
                _context.RevNiveles.Remove(revNiveles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevNivelesExists(int id)
        {
          return _context.RevNiveles.Any(e => e.IdRevNiveles == id);
        }
    }
}
