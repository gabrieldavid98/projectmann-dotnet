using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ItemInventario
    {
        public int IdItemInventario { get; set; }
        public int? FkAsignadoA { get; set; }
        public string NombreSoftware { get; set; }
        public string Licencia { get; set; }
        public string Version { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int FkUsuarioModifica { get; set; }
        public int FkUsuarioCrea { get; set; }

        public virtual Usuario FkAsignadoANavigation { get; set; }
        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
    }
}
