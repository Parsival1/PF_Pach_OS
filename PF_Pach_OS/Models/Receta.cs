using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Receta
    {
        public int IdReceta { get; set; }
        public int? CantInsumo { get; set; }
        public int? IdProducto { get; set; }
        public int? IdInsumo { get; set; }

        public virtual Insumo? IdInsumoNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
