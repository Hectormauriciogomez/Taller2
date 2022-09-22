using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class Seguros
    {
        public int IdSeguros { get; set; }
        public string Tipo { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
        public DateTime FechaVence { get; set; }
        public string? Placa { get; set; }

        public virtual Vehiculos? PlacaNavigation { get; set; }
    }
}
