using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class Repuestos
    {
        public int IdRepuestos { get; set; }
        public DateTime FechaHora { get; set; }
        public string Tipo { get; set; } = null!;
        public int Valor { get; set; }
        public string Justificacion { get; set; } = null!;
        public string Placa { get; set; } = null!;

        public virtual Vehiculos PlacaNavigation { get; set; } = null!;
    }
}
