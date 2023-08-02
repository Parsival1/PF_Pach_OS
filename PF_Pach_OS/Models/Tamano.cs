using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Tamano
    {
        public Tamano()
        {
            Productos = new HashSet<Producto>();
        }

        public byte IdTamano { get; set; }
        public byte? Tamano1 { get; set; }
        public byte? MaximoSabores { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
