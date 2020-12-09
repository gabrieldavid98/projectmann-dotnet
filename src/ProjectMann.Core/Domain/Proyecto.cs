using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            ComentarioProyectos = new HashSet<ComentarioProyecto>();
            ItemTrabajoProyectos = new HashSet<ItemTrabajoProyecto>();
        }

        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public short FkEstado { get; set; }
        public int FkCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int FkUsuarioModifica { get; set; }
        public int FkUsuarioCrea { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual Estado FkEstadoNavigation { get; set; }
        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<ComentarioProyecto> ComentarioProyectos { get; set; }
        public virtual ICollection<ItemTrabajoProyecto> ItemTrabajoProyectos { get; set; }
    }
}
