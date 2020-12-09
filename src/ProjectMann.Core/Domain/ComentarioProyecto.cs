using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ComentarioProyecto
    {
        public long IdComentarioProyecto { get; set; }
        public long FkComentario { get; set; }
        public int FkProyecto { get; set; }

        public virtual Comentario FkComentarioNavigation { get; set; }
        public virtual Proyecto FkProyectoNavigation { get; set; }
    }
}
