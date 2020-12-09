using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectMann.Core.Domain
{
    public partial class TipoIdentificacion
    {
        public TipoIdentificacion()
        {
            Clientes = new HashSet<Cliente>();
        }

        public short IdTipoIdentificacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
