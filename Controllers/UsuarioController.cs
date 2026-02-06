using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ReservasDbContext _context;

        public UsuarioController(ReservasDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            
            return View(await _context.Usuarios
                .Include(u => u.UsuarioEmails)
                .Include(u => u.Rol)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "RolId", "Nombre");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = model.Nombre,
                    RolId = model.RolId,
                    PasswordHash = model.Password, // Aquí deberías aplicar un hash real en lugar de almacenar la contraseña en texto plano
                    FechaRegistro = DateTime.Now
                };
                


                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();
                var usuarioEmail = new UsuarioEmail
                {
                    UsuarioId = nuevoUsuario.UsuarioId,
                    Email = model.Email,
                    EsPrincipal = true
                };
                _context.UsuarioEmails.Add(usuarioEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "RolId", "Nombre", model.RolId);
            return View(model);
        }
    }
}
