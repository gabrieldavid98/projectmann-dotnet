using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMann.Core.Domain;
using ProjectMann.Infrastructure.Data;
using ProjectMann.Web.Managers;

namespace ProjectMann.Web.Controllers
{
    [Authorize("AdminAndDev")]
    public class UserController : Controller
    {
        private readonly ProjectMannDbContext _context;
        private readonly IAuthManager _auth;

        public UserController(ProjectMannDbContext context, IAuthManager auth)
        {
            _context = context;
            _auth = auth;
        }

        [Authorize("AdminAndDev")]
        // GET: User
        public async Task<IActionResult> Index()
        {
            var projectMannDbContext = _context.Usuarios.Include(u => u.FkRolNavigation).Include(u => u.FkUsuarioCreaNavigation).Include(u => u.FkUsuarioModificaNavigation);
            return View(await projectMannDbContext.ToListAsync());
        }

        [Authorize("AdminAndDev")]
        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkRolNavigation)
                .Include(u => u.FkUsuarioCreaNavigation)
                .Include(u => u.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Authorize("Admin")]
        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["FkRol"] = new SelectList(_context.Rols, "IdRol", "Nombre");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Clave,FkRol,NombreUsuario,Email,Estado,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                usuario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                usuario.FkUsuarioCrea = _auth.GetCurrentUserId(HttpContext);
                usuario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkRol"] = new SelectList(_context.Rols, "IdRol", "Nombre", usuario.FkRol);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioModifica);
            return View(usuario);
        }

        // GET: User/Edit/5
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["FkRol"] = new SelectList(_context.Rols, "IdRol", "Nombre", usuario.FkRol);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioModifica);
            return View(usuario);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido,Clave,FkRol,NombreUsuario,Email,Estado,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioActual = await _context.Usuarios
                        .Where(x => x.IdUsuario == id)
                        .Select(x => new { x.FechaCreacion, x.FkUsuarioCrea })
                        .FirstOrDefaultAsync();

                    if (usuarioActual == null)
                    {
                        return NotFound();
                    }

                    usuario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                    usuario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                    usuario.FechaCreacion = usuarioActual.FechaCreacion;
                    usuario.FkUsuarioCrea = usuarioActual.FkUsuarioCrea;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            ViewData["FkRol"] = new SelectList(_context.Rols, "IdRol", "Nombre", usuario.FkRol);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", usuario.FkUsuarioModifica);
            return View(usuario);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkRolNavigation)
                .Include(u => u.FkUsuarioCreaNavigation)
                .Include(u => u.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: User/Delete/5
        [Authorize("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            
            usuario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            usuario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
