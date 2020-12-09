using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Estado
    {
        public Estado()
        {
            ItemTrabajos = new HashSet<ItemTrabajo>();
            Proyectos = new HashSet<Proyecto>();
            Tickets = new HashSet<Ticket>();
        }

        public short IdEstado { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ItemTrabajo> ItemTrabajos { get; set; }
        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
