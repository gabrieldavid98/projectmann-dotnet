using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Ticket
    {
        public Ticket()
        {
            ComentarioTickets = new HashSet<ComentarioTicket>();
        }

        public int IdTicket { get; set; }
        public int? FkAsignadoA { get; set; }
        public string Contenido { get; set; }
        public short FkPrioridad { get; set; }
        public short FkEstado { get; set; }
        public short? FkTipoTicket { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int FkUsuarioModifica { get; set; }
        public int FkUsuarioCrea { get; set; }

        public virtual Usuario FkAsignadoANavigation { get; set; }
        public virtual Estado FkEstadoNavigation { get; set; }
        public virtual Prioridad FkPrioridadNavigation { get; set; }
        public virtual TipoTicket FkTipoTicketNavigation { get; set; }
        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<ComentarioTicket> ComentarioTickets { get; set; }
    }
}
