using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class Comentario
    {
        public Comentario()
        {
            ComentarioItemTrabajos = new HashSet<ComentarioItemTrabajo>();
            ComentarioProyectos = new HashSet<ComentarioProyecto>();
            ComentarioTickets = new HashSet<ComentarioTicket>();
        }

        public long IdComentario { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int FkUsuarioModifica { get; set; }
        public int FkUsuarioCrea { get; set; }

        public virtual Usuario FkUsuarioCreaNavigation { get; set; }
        public virtual Usuario FkUsuarioModificaNavigation { get; set; }
        public virtual ICollection<ComentarioItemTrabajo> ComentarioItemTrabajos { get; set; }
        public virtual ICollection<ComentarioProyecto> ComentarioProyectos { get; set; }
        public virtual ICollection<ComentarioTicket> ComentarioTickets { get; set; }
    }
}
