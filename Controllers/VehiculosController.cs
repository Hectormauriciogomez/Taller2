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
    public class VehiculosController : Controller
    {
        private readonly DBTMecanicoCContext _context;

        public VehiculosController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            var dBTMecanicoCContext = _context.Vehiculos.Include(v => v.DocIdMecanicoNavigation).Include(v => v.DocIdPropietarioNavigation);
            return View(await dBTMecanicoCContext.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculos = await _context.Vehiculos
                .Include(v => v.DocIdMecanicoNavigation)
                .Include(v => v.DocIdPropietarioNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewData["DocIdMecanico"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion");
            ViewData["DocIdPropietario"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion");
            return View();
        }

        // POST: Vehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Tipo,Modelo,CapacidadPasajeros,CilindradaMotor,PaisOrigen,OtrasCaracteristicas,DocIdPropietario,DocIdMecanico")] Vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vehiculos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocIdMecanico"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdMecanico);
            ViewData["DocIdPropietario"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdPropietario);
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculos = await _context.Vehiculos.FindAsync(id);
            if (vehiculos == null)
            {
                return NotFound();
            }
            ViewData["DocIdMecanico"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdMecanico);
            ViewData["DocIdPropietario"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdPropietario);
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Placa,Tipo,Modelo,CapacidadPasajeros,CilindradaMotor,PaisOrigen,OtrasCaracteristicas,DocIdPropietario,DocIdMecanico")] Vehiculos vehiculos)
        {
            if (id != vehiculos.Placa)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculosExists(vehiculos.Placa))
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
            ViewData["DocIdMecanico"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdMecanico);
            ViewData["DocIdPropietario"] = new SelectList(_context.Personas, "DocIdentificacion", "DocIdentificacion", vehiculos.DocIdPropietario);
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculos = await _context.Vehiculos
                .Include(v => v.DocIdMecanicoNavigation)
                .Include(v => v.DocIdPropietarioNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vehiculos == null)
            {
                return Problem("Entity set 'DBTMecanicoCContext.Vehiculos'  is null.");
            }
            var vehiculos = await _context.Vehiculos.FindAsync(id);
            if (vehiculos != null)
            {
                _context.Vehiculos.Remove(vehiculos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculosExists(string id)
        {
          return _context.Vehiculos.Any(e => e.Placa == id);
        }
    }
}
