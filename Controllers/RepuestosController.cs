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
    public class RepuestosController : Controller
    {
        private readonly DBTMecanicoCContext _context;

        public RepuestosController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        // GET: Repuestos
        public async Task<IActionResult> Index()
        {
            var dBTMecanicoCContext = _context.Repuestos.Include(r => r.PlacaNavigation);
            return View(await dBTMecanicoCContext.ToListAsync());
        }

        // GET: Repuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Repuestos == null)
            {
                return NotFound();
            }

            var repuestos = await _context.Repuestos
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdRepuestos == id);
            if (repuestos == null)
            {
                return NotFound();
            }

            return View(repuestos);
        }

        // GET: Repuestos/Create
        public IActionResult Create()
        {
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa");
            return View();
        }

        // POST: Repuestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRepuestos,FechaHora,Tipo,Valor,Justificacion,Placa")] Repuestos repuestos)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(repuestos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", repuestos.Placa);
            return View(repuestos);
        }

        // GET: Repuestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Repuestos == null)
            {
                return NotFound();
            }

            var repuestos = await _context.Repuestos.FindAsync(id);
            if (repuestos == null)
            {
                return NotFound();
            }
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", repuestos.Placa);
            return View(repuestos);
        }

        // POST: Repuestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRepuestos,FechaHora,Tipo,Valor,Justificacion,Placa")] Repuestos repuestos)
        {
            if (id != repuestos.IdRepuestos)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(repuestos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestosExists(repuestos.IdRepuestos))
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
            ViewData["Placa"] = new SelectList(_context.Vehiculos, "Placa", "Placa", repuestos.Placa);
            return View(repuestos);
        }

        // GET: Repuestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Repuestos == null)
            {
                return NotFound();
            }

            var repuestos = await _context.Repuestos
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.IdRepuestos == id);
            if (repuestos == null)
            {
                return NotFound();
            }

            return View(repuestos);
        }

        // POST: Repuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Repuestos == null)
            {
                return Problem("Entity set 'DBTMecanicoCContext.Repuestos'  is null.");
            }
            var repuestos = await _context.Repuestos.FindAsync(id);
            if (repuestos != null)
            {
                _context.Repuestos.Remove(repuestos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestosExists(int id)
        {
          return _context.Repuestos.Any(e => e.IdRepuestos == id);
        }
    }
}
