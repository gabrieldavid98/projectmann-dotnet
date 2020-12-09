using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Usuario
    {
        public Usuario()
        {
            ClienteFkUsuarioCreaNavigations = new HashSet<Cliente>();
            ClienteFkUsuarioModificaNavigations = new HashSet<Cliente>();
            ComentarioFkUsuarioCreaNavigations = new HashSet<Comentario>();
            ComentarioFkUsuarioModificaNavigations = new HashSet<Comentario>();
            InverseFkUsuarioCreaNavigation = new HashSet<Usuario>();
            InverseFkUsuarioModificaNavigation = new HashSet<Usuario>();
            ItemInventarioFkAsignadoANavigations = new HashSet<ItemInventario>();
            ItemInventarioFkUsuarioCreaNavigations = new HashSet<ItemInventario>();
            ItemInventarioFkUsuarioModificaNavigations = new HashSet<ItemInventario>();
            ItemTrabajoFkAsignadoANavigations = new HashSet<ItemTrabajo>();
            ItemTrabajoFkUsuarioCreaNavigations = new HashSet<ItemTrabajo>();
            ItemTrabajoFkUsuarioModificaNavigations = new HashSet<ItemTrabajo>();
            ProyectoFkUsuarioCreaNavigations = new HashSet<Proyecto>();
            ProyectoFkUsuarioModificaNavigations = new HashSet<Proyecto>();
            TicketFkAsignadoANavigations = new HashSet<Ticket>();
            TicketFkUsuarioCreaNavigations = new HashSet<Ticket>();
            TicketFkUsuarioModificaNavigations = new HashSet<Ticket>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Clave { get; set; }
        public short FkRol { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public bool? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? FkUsuarioModifica { get; set; }
        public int? FkUsuarioCrea { get; set; }

        public virtual Rol FkRolNavigation { get; set; }
        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<Cliente> ClienteFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<Cliente> ClienteFkUsuarioModificaNavigations { get; set; }
        public virtual ICollection<Comentario> ComentarioFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<Comentario> ComentarioFkUsuarioModificaNavigations { get; set; }
        public virtual ICollection<Usuario> InverseFkUsuarioCreaNavigation { get; set; }
        public virtual ICollection<Usuario> InverseFkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<ItemInventario> ItemInventarioFkAsignadoANavigations { get; set; }
        public virtual ICollection<ItemInventario> ItemInventarioFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<ItemInventario> ItemInventarioFkUsuarioModificaNavigations { get; set; }
        public virtual ICollection<ItemTrabajo> ItemTrabajoFkAsignadoANavigations { get; set; }
        public virtual ICollection<ItemTrabajo> ItemTrabajoFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<ItemTrabajo> ItemTrabajoFkUsuarioModificaNavigations { get; set; }
        public virtual ICollection<Proyecto> ProyectoFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<Proyecto> ProyectoFkUsuarioModificaNavigations { get; set; }
        public virtual ICollection<Ticket> TicketFkAsignadoANavigations { get; set; }
        public virtual ICollection<Ticket> TicketFkUsuarioCreaNavigations { get; set; }
        public virtual ICollection<Ticket> TicketFkUsuarioModificaNavigations { get; set; }
    }
}
