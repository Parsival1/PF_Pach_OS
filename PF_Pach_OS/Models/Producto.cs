using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
            Receta = new HashSet<Receta>();
        }

        public int IdProducto { get; set; }
        public string? NomProducto { get; set; }
        public int? PrecioVenta { get; set; }
        public string? Estado { get; set; }
        public byte? IdTamano { get; set; }
        public byte? IdCategoria { get; set; }

        public virtual Categoria? IdCategoriaNavigation { get; set; }
        public virtual Tamano? IdTamanoNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
