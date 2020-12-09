using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class TipoItemTrabajo
    {
        public TipoItemTrabajo()
        {
            ItemTrabajos = new HashSet<ItemTrabajo>();
        }

        public short IdTipoItemTrabajo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ItemTrabajo> ItemTrabajos { get; set; }
    }
}
