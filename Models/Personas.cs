using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class Personas
    {
        public Personas()
        {
            VehiculoDocIdMecanicoNavigations = new HashSet<Vehiculos>();
            VehiculoDocIdPropietarioNavigations = new HashSet<Vehiculos>();
        }

        public string DocIdentificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Credencial { get; set; } = null!;
        public int IdRol { get; set; }
        public string CiudadDireccion { get; set; } = null!;
        public string CorreoNivelEstudio { get; set; } = null!;

        public virtual Roles IdRolNavigation { get; set; } = null!;
        public virtual ICollection<Vehiculos> VehiculoDocIdMecanicoNavigations { get; set; }
        public virtual ICollection<Vehiculos> VehiculoDocIdPropietarioNavigations { get; set; }
    }
}
