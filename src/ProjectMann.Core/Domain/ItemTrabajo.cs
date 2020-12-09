using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ItemTrabajo
    {
        public ItemTrabajo()
        {
            ComentarioItemTrabajos = new HashSet<ComentarioItemTrabajo>();
            ItemTrabajoEnlaceFkItemTrabajo1Navigations = new HashSet<ItemTrabajoEnlace>();
            ItemTrabajoEnlaceFkItemTrabajo2Navigations = new HashSet<ItemTrabajoEnlace>();
            ItemTrabajoProyectos = new HashSet<ItemTrabajoProyecto>();
        }

        public int IdItemTrabajo { get; set; }
        public short FkTipoItemTrabajo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public short FkEstado { get; set; }
        public int? FkAsignadoA { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int FkUsuarioModifica { get; set; }
        public int FkUsuarioCrea { get; set; }

        public virtual Usuario FkAsignadoANavigation { get; set; }
        public virtual Estado FkEstadoNavigation { get; set; }
        public virtual TipoItemTrabajo FkTipoItemTrabajoNavigation { get; set; }
        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<ComentarioItemTrabajo> ComentarioItemTrabajos { get; set; }
        public virtual ICollection<ItemTrabajoEnlace> ItemTrabajoEnlaceFkItemTrabajo1Navigations { get; set; }
        public virtual ICollection<ItemTrabajoEnlace> ItemTrabajoEnlaceFkItemTrabajo2Navigations { get; set; }
        public virtual ICollection<ItemTrabajoProyecto> ItemTrabajoProyectos { get; set; }
    }
}
