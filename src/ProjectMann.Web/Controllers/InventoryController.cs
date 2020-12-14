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
    public class InventoryController : Controller
    {
        private readonly ProjectMannDbContext _context;
        private readonly IAuthManager _auth;

        public InventoryController(ProjectMannDbContext context, IAuthManager auth)
        {
            _context = context;
            _auth = auth;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            var projectMannDbContext = _context.ItemInventarios.Include(i => i.FkAsignadoANavigation).Include(i => i.FkUsuarioCreaNavigation).Include(i => i.FkUsuarioModificaNavigation);
            return View(await projectMannDbContext.ToListAsync());
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventarios
                .Include(i => i.FkAsignadoANavigation)
                .Include(i => i.FkUsuarioCreaNavigation)
                .Include(i => i.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdItemInventario == id);
            if (itemInventario == null)
            {
                return NotFound();
            }

            return View(itemInventario);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItemInventario,FkAsignadoA,NombreSoftware,Licencia,Version,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] ItemInventario itemInventario)
        {
            if (ModelState.IsValid)
            {
                itemInventario.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                itemInventario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                itemInventario.FkUsuarioCrea = _auth.GetCurrentUserId(HttpContext);
                itemInventario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);

                _context.Add(itemInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioModifica);
            return View(itemInventario);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventarios.FindAsync(id);
            if (itemInventario == null)
            {
                return NotFound();
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioModifica);
            return View(itemInventario);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItemInventario,FkAsignadoA,NombreSoftware,Licencia,Version,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] ItemInventario itemInventario)
        {
            if (id != itemInventario.IdItemInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var inventarioActual = await _context.ItemInventarios
                        .Where(x => x.IdItemInventario == id)
                        .Select(x => new { x.FechaCreacion, x.FkUsuarioCrea })
                        .FirstOrDefaultAsync();

                    if (inventarioActual == null)
                    {
                        return NotFound();
                    }

                    itemInventario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
                    itemInventario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
                    itemInventario.FechaCreacion = inventarioActual.FechaCreacion;
                    itemInventario.FkUsuarioCrea = inventarioActual.FkUsuarioCrea;

                    _context.Update(itemInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInventarioExists(itemInventario.IdItemInventario))
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", itemInventario.FkUsuarioModifica);
            return View(itemInventario);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventarios
                .Include(i => i.FkAsignadoANavigation)
                .Include(i => i.FkUsuarioCreaNavigation)
                .Include(i => i.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdItemInventario == id);
            if (itemInventario == null)
            {
                return NotFound();
            }

            return View(itemInventario);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemInventario = await _context.ItemInventarios.FindAsync(id);
            itemInventario.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            itemInventario.FkUsuarioModifica = _auth.GetCurrentUserId(HttpContext);
            _context.ItemInventarios.Remove(itemInventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInventarioExists(int id)
        {
            return _context.ItemInventarios.Any(e => e.IdItemInventario == id);
        }
    }
}
