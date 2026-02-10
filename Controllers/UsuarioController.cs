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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Usuarios.FindAsync(id);
            ViewData["Roles"] = new SelectList(_context.Roles, "RolId", "Nombre");
            if (recurso == null)
            {
                return NotFound();
            }
            return View(recurso);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,FechaRegistro,Rol,EmailPrincipal")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
             return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
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
            return View(usuario);

        }

        private bool UsuarioExists(int usuarioId)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == usuarioId);
        }
    }
}
