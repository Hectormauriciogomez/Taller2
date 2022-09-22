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
    public class SegurosController : Controller
    {
        private readonly DBTMecanicoCContext _context;

        public SegurosController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        // GET: Seguros
        public async Task<IActionResult> Index()
        {
            var dBTMecanicoCContext = _context.Seguros.Include(s => s.PlacaNavigation);
            return View(await dBTMecanicoCContext.ToListAsync());
        }

        // GET: Seguros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros
                .Include(s => s.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdSeguros == id);
            if (seguros == null)
            {
                return NotFound();
            }

            return View(seguros);
        }

        // GET: Seguros/Create
        public IActionResult Create()
        {
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa");
            return View();
        }

        // POST: Seguros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeguros,Tipo,FechaCompra,FechaVence,Placa")] Seguros seguros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", seguros.Placa);
            return View(seguros);
        }

        // GET: Seguros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros.FindAsync(id);
            if (seguros == null)
            {
                return NotFound();
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", seguros.Placa);
            return View(seguros);
        }

        // POST: Seguros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeguros,Tipo,FechaCompra,FechaVence,Placa")] Seguros seguros)
        {
            if (id != seguros.IdSeguros)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegurosExists(seguros.IdSeguros))
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
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", seguros.Placa);
            return View(seguros);
        }

        // GET: Seguros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros
                .Include(s => s.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdSeguros == id);
            if (seguros == null)
            {
                return NotFound();
            }

            return View(seguros);
        }

        // POST: Seguros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seguros == null)
            {
                return Problem("Entity set 'DBTMecanicoCContext.Seguros'  is null.");
            }
            var seguros = await _context.Seguros.FindAsync(id);
            if (seguros != null)
            {
                _context.Seguros.Remove(seguros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegurosExists(int id)
        {
          return _context.Seguros.Any(e => e.IdSeguros == id);
        }
    }
}
