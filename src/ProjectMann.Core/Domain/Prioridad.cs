using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Tickets = new HashSet<Ticket>();
        }

        public short IdPrioridad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
