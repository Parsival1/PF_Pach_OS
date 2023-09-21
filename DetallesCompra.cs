using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PF_Pach_OS.Models
{
    public partial class DetallesCompra
    {
        
        public int IdDetallesCompra { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int? PrecioInsumo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int? Cantidad { get; set; }
        public int? IdCompra { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Medida { get; set; }
        public int? IdInsumo { get; set; }

        public virtual Compra? IdCompraNavigation { get; set; }
        public virtual Insumo? IdInsumoNavigation { get; set; }
    }
}
