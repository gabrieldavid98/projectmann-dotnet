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
    public class WorkItemController : Controller
    {
        private readonly ProjectMannDbContext _context;
        private readonly IAuthManager _auth;

        public WorkItemController(ProjectMannDbContext context,  IAuthManager auth)
        {
            _context = context;
            _auth = auth;
        }

        // GET: WorkItem
        public async Task<IActionResult> Index()
        {
            var projectMannDbContext = _context.ItemTrabajos
                .Include(i => i.FkAsignadoANavigation)
                .Include(i => i.FkEstadoNavigation)
                .Include(i => i.FkTipoItemTrabajoNavigation)
                .Include(i => i.FkUsuarioCreaNavigation)
                .Include(i => i.FkUsuarioModificaNavigation)
                .Where(x => x.FkEstado != 7);

            return View(await projectMannDbContext.ToListAsync());
        }

        // GET: WorkItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTrabajo = await _context.ItemTrabajos
                .Include(i => i.FkAsignadoANavigation)
                .Include(i => i.FkEstadoNavigation)
                .Include(i => i.FkTipoItemTrabajoNavigation)
                .Include(i => i.FkUsuarioCreaNavigation)
                .Include(i => i.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdItemTrabajo == id);
            if (itemTrabajo == null)
            {
                return NotFound();
            }

            return View(itemTrabajo);
        }

        // GET: WorkItem/Create
        public IActionResult Create()
        {
            ViewData["FkAsignadoA"] = new SelectList(
                _context.Usuarios
                    .Where(u => u.FkRol == 2 && u.Estado == true)
                    .Select(u => new 
                    { 
                        u.IdUsuario, 
                        Nombre = $"{u.Nombre} {u.Apellido}".Trim()
                    }), 
                "IdUsuario", 
                "Nombre"
            );

            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");
            ViewData["FkTipoItemTrabajo"] = new SelectList(_context.TipoItemTrabajos, "IdTipoItemTrabajo", "Nombre");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: WorkItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItemTrabajo,FkTipoItemTrabajo,Titulo,Descripcion,FkEstado,FkAsignadoA,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] ItemTrabajo itemTrabajo)
        {
            if (ModelState.IsValid)
            {
                itemTrabajo.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                itemTrabajo.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                itemTrabajo.FkUsuarioCrea = _auth.GetCurrentUserId(HttpContext); 
                itemTrabajo.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                itemTrabajo.FkEstado = 1;

                _context.Add(itemTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkAsignadoA);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", itemTrabajo.FkEstado);
            ViewData["FkTipoItemTrabajo"] = new SelectList(_context.TipoItemTrabajos, "IdTipoItemTrabajo", "Nombre", itemTrabajo.FkTipoItemTrabajo);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioModifica);
            return View(itemTrabajo);
        }

        // GET: WorkItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTrabajo = await _context.ItemTrabajos.FindAsync(id);
            if (itemTrabajo == null)
            {
                return NotFound();
            }

            ViewData["FkAsignadoA"] = new SelectList(
                _context.Usuarios
                    .Where(u => u.FkRol == 2 && u.Estado == true)
                    .Select(u => new 
                    { 
                        u.IdUsuario, 
                        Nombre = $"{u.Nombre} {u.Apellido}".Trim()
                    }), 
                "IdUsuario", 
                "Nombre",
                itemTrabajo.FkAsignadoA
            );

            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", itemTrabajo.FkEstado);
            ViewData["FkTipoItemTrabajo"] = new SelectList(_context.TipoItemTrabajos, "IdTipoItemTrabajo", "Nombre", itemTrabajo.FkTipoItemTrabajo);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioModifica);
            return View(itemTrabajo);
        }

        // POST: WorkItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItemTrabajo,FkTipoItemTrabajo,Titulo,Descripcion,FkEstado,FkAsignadoA,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] ItemTrabajo itemTrabajo)
        {
            if (id != itemTrabajo.IdItemTrabajo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var itemTrabajoActual = await _context.ItemTrabajos
                        .Where(x => x.IdItemTrabajo == id)
                        .Select(x => new { x.FechaCreacion, x.FkUsuarioCrea })
                        .FirstOrDefaultAsync();

                    if (itemTrabajoActual == null)
                    {
                        return NotFound();
                    }

                    itemTrabajo.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                    itemTrabajo.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                    itemTrabajo.FechaCreacion = itemTrabajoActual.FechaCreacion;
                    itemTrabajo.FkUsuarioCrea = itemTrabajoActual.FkUsuarioCrea;

                    _context.Update(itemTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTrabajoExists(itemTrabajo.IdItemTrabajo))
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkAsignadoA);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", itemTrabajo.FkEstado);
            ViewData["FkTipoItemTrabajo"] = new SelectList(_context.TipoItemTrabajos, "IdTipoItemTrabajo", "Nombre", itemTrabajo.FkTipoItemTrabajo);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemTrabajo.FkUsuarioModifica);
            return View(itemTrabajo);
        }

        // GET: WorkItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTrabajo = await _context.ItemTrabajos
                .Include(i => i.FkAsignadoANavigation)
                .Include(i => i.FkEstadoNavigation)
                .Include(i => i.FkTipoItemTrabajoNavigation)
                .Include(i => i.FkUsuarioCreaNavigation)
                .Include(i => i.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdItemTrabajo == id);
            if (itemTrabajo == null)
            {
                return NotFound();
            }

            return View(itemTrabajo);
        }

        // POST: WorkItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemTrabajo = await _context.ItemTrabajos.FindAsync(id);
            itemTrabajo.FkEstado = 7;
            itemTrabajo.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            itemTrabajo.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTrabajoExists(int id)
        {
            return _context.ItemTrabajos.Any(e => e.IdItemTrabajo == id);
        }
    }
}
