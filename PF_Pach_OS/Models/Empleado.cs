using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdEmpleado { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Contrasena { get; set; }
        public string? Celular { get; set; }
        public string? Correo { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
