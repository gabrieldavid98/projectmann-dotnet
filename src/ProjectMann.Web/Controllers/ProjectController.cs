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
    public class ProjectController : Controller
    {
        private readonly ProjectMannDbContext _context;
        private readonly IAuthManager _auth;

        public ProjectController(ProjectMannDbContext context, IAuthManager auth)
        {
            _context = context;
            _auth = auth;
        }

        [Authorize("AdminAndDev")]
        // GET: Project
        public async Task<IActionResult> Index()
        {
            var projectMannDbContext = _context.Proyectos.Include(p => p.FkClienteNavigation).Include(p => p.FkEstadoNavigation).Include(p => p.FkUsuarioCreaNavigation).Include(p => p.FkUsuarioModificaNavigation);
            return View(await projectMannDbContext.ToListAsync());
        }

        [Authorize("AdminAndDev")]
        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.FkClienteNavigation)
                .Include(p => p.FkEstadoNavigation)
                .Include(p => p.FkUsuarioCreaNavigation)
                .Include(p => p.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdProyecto == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        [Authorize("Admin")]
        // GET: Project/Create
        public IActionResult Create()
        {
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProyecto,NombreProyecto,FkEstado,FkCliente,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                proyecto.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                proyecto.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                proyecto.FkUsuarioCrea = _auth.GetCurrentUserId(HttpContext);
                proyecto.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                proyecto.FkEstado = 1;

                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioModifica);
            return View(proyecto);
        }

        // GET: Project/Edit/5
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioModifica);
            return View(proyecto);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProyecto,NombreProyecto,FkEstado,FkCliente,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Proyecto proyecto)
        {
            if (id != proyecto.IdProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var proyectoActual = await _context.Proyectos
                        .Where(x => x.IdProyecto == id)
                        .Select(x => new { x.FechaCreacion, x.FkUsuarioCrea })
                        .FirstOrDefaultAsync();

                    if (proyectoActual == null)
                    {
                        return NotFound();
                    }

                    proyecto.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                    proyecto.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                    proyecto.FechaCreacion = proyectoActual.FechaCreacion;
                    proyecto.FkUsuarioCrea = proyectoActual.FkUsuarioCrea;
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.IdProyecto))
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", proyecto.FkUsuarioModifica);
            return View(proyecto);
        }

        // GET: Project/Delete/5
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.FkClienteNavigation)
                .Include(p => p.FkEstadoNavigation)
                .Include(p => p.FkUsuarioCreaNavigation)
                .Include(p => p.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdProyecto == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Project/Delete/5
        [Authorize("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            proyecto.FkEstado = 7;
            proyecto.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            proyecto.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.IdProyecto == id);
        }
    }
}
