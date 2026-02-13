using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models.Entities;

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

            var usuario = await _context.Usuarios
                .Include(u => u.UsuarioEmails)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
            
            if (usuario == null)
            {
                return NotFound();
            }
            ViewBag.Roles= new SelectList(_context.Roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,FechaRegistro,RolId")] Usuario usuario, string emailPrincipal)
        {
            if (id != usuario.UsuarioId)
            {
             return NotFound();

            }
            ModelState.Remove("Rol");
            ModelState.Remove("PasswordHash");
            ModelState.Remove("usuarioEmails");


            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioEnDb = await _context.Usuarios
                        .Include(u => u.UsuarioEmails)
                        .FirstOrDefaultAsync(u => u.UsuarioId == id);


                    if (usuarioEnDb == null) return NotFound();

                    usuarioEnDb.Nombre = usuario.Nombre;
                    usuarioEnDb.RolId = usuario.RolId;
                    usuarioEnDb.FechaRegistro = usuario.FechaRegistro;


                    var correoPrincipal = usuarioEnDb.UsuarioEmails
                .FirstOrDefault(e => e.EsPrincipal == true);

                    if (correoPrincipal != null)
                    {
                        correoPrincipal.Email = emailPrincipal;
                    }
                    else
                    {
                        usuarioEnDb.UsuarioEmails.Add(new UsuarioEmail
                        {
                            Email = emailPrincipal,
                            EsPrincipal = true
                        });
                    }

                   
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
               
            }
            ViewBag.Roles = new SelectList(_context.Roles, "RolId", "Nombre", usuario.RolId);
            ViewBag.EmailEscrito = emailPrincipal;
            return View(usuario);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.UsuarioEmails)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(e => e.UsuarioId == id);

            if(usuario == null)
            {
                return NotFound();
            }
           
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
              
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int usuarioId)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == usuarioId);
        }
    }
}
