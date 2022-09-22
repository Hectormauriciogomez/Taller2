using System;
using System.Collections.Generic;

namespace TMecanicoC.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Personas = new HashSet<Personas>();
        }

        public int IdRol { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Autorizacion { get; set; } = null!;

        public virtual ICollection<Personas> Personas { get; set; }
    }
}
