using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ItemTrabajoProyecto
    {
        public int IdItemTrabajoProyecto { get; set; }
        public int FkProyecto { get; set; }
        public int FkItemTrabajo { get; set; }

        public virtual ItemTrabajo FkItemTrabajoNavigation { get; set; }
        public virtual Proyecto FkProyectoNavigation { get; set; }
    }
}
