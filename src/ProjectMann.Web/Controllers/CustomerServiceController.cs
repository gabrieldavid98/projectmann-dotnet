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
    [Authorize]
    public class CustomerServiceController : Controller
    {
        private readonly ProjectMannDbContext _context;

        public CustomerServiceController(ProjectMannDbContext context)
        {
            _context = context;
        }

        // GET: CustomerService
        public async Task<IActionResult> Index()
        {
            var projectMannDbContext = _context.Tickets.Include(t => t.FkAsignadoANavigation).Include(t => t.FkEstadoNavigation).Include(t => t.FkPrioridadNavigation).Include(t => t.FkTipoTicketNavigation).Include(t => t.FkUsuarioCreaNavigation).Include(t => t.FkUsuarioModificaNavigation);
            return View(await projectMannDbContext.ToListAsync());
        }

        // GET: CustomerService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.FkAsignadoANavigation)
                .Include(t => t.FkEstadoNavigation)
                .Include(t => t.FkPrioridadNavigation)
                .Include(t => t.FkTipoTicketNavigation)
                .Include(t => t.FkUsuarioCreaNavigation)
                .Include(t => t.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: CustomerService/Create
        public IActionResult Create()
        {
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");
            ViewData["FkPrioridad"] = new SelectList(_context.Prioridads, "IdPrioridad", "Nombre");
            ViewData["FkTipoTicket"] = new SelectList(_context.TipoTickets, "IdTipoTicket", "Nombre");
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return View();
        }

        // POST: CustomerService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTicket,FkAsignadoA,Contenido,FkPrioridad,FkEstado,FkTipoTicket,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkAsignadoA);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", ticket.FkEstado);
            ViewData["FkPrioridad"] = new SelectList(_context.Prioridads, "IdPrioridad", "Nombre", ticket.FkPrioridad);
            ViewData["FkTipoTicket"] = new SelectList(_context.TipoTickets, "IdTipoTicket", "Nombre", ticket.FkTipoTicket);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioModifica);
            return View(ticket);
        }

        // GET: CustomerService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkAsignadoA);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", ticket.FkEstado);
            ViewData["FkPrioridad"] = new SelectList(_context.Prioridads, "IdPrioridad", "Nombre", ticket.FkPrioridad);
            ViewData["FkTipoTicket"] = new SelectList(_context.TipoTickets, "IdTipoTicket", "Nombre", ticket.FkTipoTicket);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioModifica);
            return View(ticket);
        }

        // POST: CustomerService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTicket,FkAsignadoA,Contenido,FkPrioridad,FkEstado,FkTipoTicket,FechaCreacion,FechaModificacion,FkUsuarioModifica,FkUsuarioCrea")] Ticket ticket)
        {
            if (id != ticket.IdTicket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.IdTicket))
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
            ViewData["FkAsignadoA"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkAsignadoA);
            ViewData["FkEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre", ticket.FkEstado);
            ViewData["FkPrioridad"] = new SelectList(_context.Prioridads, "IdPrioridad", "Nombre", ticket.FkPrioridad);
            ViewData["FkTipoTicket"] = new SelectList(_context.TipoTickets, "IdTipoTicket", "Nombre", ticket.FkTipoTicket);
            ViewData["FkUsuarioCrea"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioCrea);
            ViewData["FkUsuarioModifica"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", ticket.FkUsuarioModifica);
            return View(ticket);
        }

        // GET: CustomerService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.FkAsignadoANavigation)
                .Include(t => t.FkEstadoNavigation)
                .Include(t => t.FkPrioridadNavigation)
                .Include(t => t.FkTipoTicketNavigation)
                .Include(t => t.FkUsuarioCreaNavigation)
                .Include(t => t.FkUsuarioModificaNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: CustomerService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.IdTicket == id);
        }
    }
}
