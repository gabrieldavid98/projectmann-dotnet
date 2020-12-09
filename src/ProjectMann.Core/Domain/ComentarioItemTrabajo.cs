using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ComentarioItemTrabajo
    {
        public long IdComentarioItemTrabajo { get; set; }
        public long FkComentario { get; set; }
        public int FkItemTrabajo { get; set; }

        public virtual Comentario FkComentarioNavigation { get; set; }
        public virtual ItemTrabajo FkItemTrabajoNavigation { get; set; }
    }
}
