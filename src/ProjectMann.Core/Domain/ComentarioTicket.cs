using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class ComentarioTicket
    {
        public long IdComentarioTicket { get; set; }
        public long FkComentario { get; set; }
        public int FkTicket { get; set; }

        public virtual Comentario FkComentarioNavigation { get; set; }
        public virtual Ticket FkTicketNavigation { get; set; }
    }
}
