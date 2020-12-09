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

namespace ProjectMann.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectMannDbContext _context;

        public ProjectController(ProjectMannDbContext context)
        {
            _context = context;
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Celular");
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
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Celular", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioModifica);
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Celular", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioModifica);
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "IdCliente", "Celular", proyecto.FkCliente);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", proyecto.FkEstado);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", proyecto.FkUsuarioModifica);
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
            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.IdProyecto == id);
        }
    }
}
