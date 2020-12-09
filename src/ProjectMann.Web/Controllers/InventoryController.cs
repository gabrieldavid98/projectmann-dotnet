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
    [Authorize("AdminAndDev")]
    public class InventoryController : Controller
    {
        private readonly ProjectMannDbContext _context;

        public InventoryController(ProjectMannDbContext context)
        {
            _context = context;
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
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
                _context.Add(itemInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioModifica);
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioModifica);
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkAsignadoA);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", itemInventario.FkUsuarioModifica);
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
