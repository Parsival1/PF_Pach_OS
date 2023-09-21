using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Insumo
    {
        public Insumo()
        {
            DetallesCompras = new HashSet<DetallesCompra>();
            Receta = new HashSet<Receta>();
        }

        public int IdInsumo { get; set; }
        public string? NomInsumo { get; set; }
        [Remote("IsUnique", "Insumos", ErrorMessage = "Este nombre de insumo ya ha sido registrado.")]
        public int? CantInsumo { get; set; }
        public string? Medida { get; set; }
        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; }
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
