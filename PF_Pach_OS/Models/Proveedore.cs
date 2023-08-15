using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProveedor { get; set; }
        public string? Nit { get; set; }
        public string? NomLocal { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
