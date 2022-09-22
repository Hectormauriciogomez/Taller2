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
    public class PersonasController : Controller
    {
        private readonly DBTMecanicoCContext _context;
        public PersonasController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        { // Obtiene de la base de datos la información de la tabla Personas incluyendo la información de la tabla Roles
            var dBTMecanicoCContext = _context.Personas.Include(p => p.IdRolNavigation);
            return View(await dBTMecanicoCContext.ToListAsync());
        }

        // GET: Personas/Create
        public IActionResult Create()
        { // Obtiene la información de la tabla Roles
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View();
        }

        // POST: Personas/Create
        [HttpPost]   // Envía información de un nuevo registro a la base de datos
        [ValidateAntiForgeryToken]   //protege la aplicación contra la falsificación de solicitudes entre sitios. 
        public async Task<IActionResult> Create([Bind("DocIdentificacion,Nombres,Apellidos,Credencial,IdRol,CiudadDireccion,CorreoNivelEstudio")] Personas personas)
        {
            if (!ModelState.IsValid)  // Valida si la información de los campos se ajusta a las condiciones básicas
            {
                _context.Add(personas);                 // Prepara el registro para adicionarlo en la tabla Personas
                await _context.SaveChangesAsync();      // Adiciona el registro
                return RedirectToAction(nameof(Index));  // Retorna a la vista inicial
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", personas.IdRol); 
            return View(personas);  // Si el registro no es correcto se devuelve a la vista Crear indicando los errores
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var personas = await _context.Personas.FindAsync(id);
            if (personas == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", personas.IdRol);
            return View(personas);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DocIdentificacion,Nombres,Apellidos,Credencial,IdRol,CiudadDireccion,CorreoNivelEstudio")] Personas personas)
        {
            if (id != personas.DocIdentificacion)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(personas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonasExists(personas.DocIdentificacion))
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
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", personas.IdRol);
            return View(personas);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var personas = await _context.Personas
                .Include(p => p.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.DocIdentificacion == id);
            if (personas == null)
            {
                return NotFound();
            }

            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'DBTMecanicoCContext.Personas'  is null.");
            }
            var personas = await _context.Personas.FindAsync(id);
            if (personas != null)
            {
                _context.Personas.Remove(personas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonasExists(string id)
        {
          return (_context.Personas?.Any(e => e.DocIdentificacion == id)).GetValueOrDefault();
        }
    }
}
