using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public DateTime? FechaVenta { get; set; }
        public int? TotalVenta { get; set; }
        public string? TipoPago { get; set; }
        public int? Pago { get; set; }
        public int? PagoDomicilio { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
