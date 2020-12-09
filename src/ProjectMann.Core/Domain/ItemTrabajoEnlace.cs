using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ItemTrabajoEnlace
    {
        public int IdItemTrabajoEnlace { get; set; }
        public int FkItemTrabajo1 { get; set; }
        public int FkItemTrabajo2 { get; set; }
        public short FkTipoEnlace { get; set; }

        public virtual ItemTrabajo FkItemTrabajo1Navigation { get; set; }
        public virtual ItemTrabajo FkItemTrabajo2Navigation { get; set; }
        public virtual TipoEnlace FkTipoEnlaceNavigation { get; set; }
    }
}
