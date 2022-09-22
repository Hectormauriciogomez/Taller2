using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class Vehiculos
    {
        public Vehiculos()
        {
            Repuestos = new HashSet<Repuestos>();
            RevNiveles = new HashSet<RevNiveles>();
            Seguros = new HashSet<Seguros>();
        }

        public string Placa { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string CapacidadPasajeros { get; set; } = null!;
        public string CilindradaMotor { get; set; } = null!;
        public string? PaisOrigen { get; set; }
        public string? OtrasCaracteristicas { get; set; }
        public string DocIdPropietario { get; set; } = null!;
        public string DocIdMecanico { get; set; } = null!;

        public virtual Personas DocIdMecanicoNavigation { get; set; } = null!;
        public virtual Personas DocIdPropietarioNavigation { get; set; } = null!;
        public virtual ICollection<Repuestos> Repuestos { get; set; }
        public virtual ICollection<RevNiveles> RevNiveles { get; set; }
        public virtual ICollection<Seguros> Seguros { get; set; }
    }
}
