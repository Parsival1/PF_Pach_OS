using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class DetallesCompra
    {
        public int IdDetallesCompra { get; set; }
        public int? PrecioInsumo { get; set; }
        public int? Cantidad { get; set; }
        public string? Medida { get; set; }
        public int? IdCompra { get; set; }
        public int? IdInsumo { get; set; }

        public virtual Compra? IdCompraNavigation { get; set; }
        public virtual Insumo? IdInsumoNavigation { get; set; }
    }
}
