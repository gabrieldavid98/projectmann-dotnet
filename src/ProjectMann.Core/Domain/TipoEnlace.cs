using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class TipoEnlace
    {
        public TipoEnlace()
        {
            ItemTrabajoEnlaces = new HashSet<ItemTrabajoEnlace>();
        }

        public short IdTipoEnlace { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ItemTrabajoEnlace> ItemTrabajoEnlaces { get; set; }
    }
}
