//Este controlador simula el formulario de logueo para acceder al sistema
using Microsoft.AspNetCore.Mvc;
using TMecanicoC.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TMecanicoC.Controllers
{
    public class LoginController : Controller
    {
        // variable privada que hace referencia a nuestro contexto de base de datos
        private readonly DBTMecanicoCContext _context;
        public LoginController(DBTMecanicoCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtener la lista de menus y con include la lista de submenus
            List<Menu> menulista = _context.Menus.Include(m => m.InverseIdMenuPadreNavigation)
                //Obtener los menus padre que no tengan submenus
                .Where(m => m.IdMenuPadre==null).ToList();

            // Opciones para serializar Json
            var options = new JsonSerializerOptions
            {
                // como un submebu puede tener submenus, se debe ignorar ese evento
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
            };

            // Crear una sesion llamada menu la cual almacena menulista con esas opciones
            HttpContext.Session.SetString("menu", JsonSerializer.Serialize(menulista, options));


            return View();
        }
    }
}
