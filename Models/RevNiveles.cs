using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class RevNiveles
    {
        public int IdRevNiveles { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal NivelAceite { get; set; }
        public decimal NivelFrenos { get; set; }
        public decimal NivelRefrigerante { get; set; }
        public decimal NivelDireccion { get; set; }
        public string Placa { get; set; } = null!;

        public virtual Vehiculos PlacaNavigation { get; set; } = null!;
    }
}
