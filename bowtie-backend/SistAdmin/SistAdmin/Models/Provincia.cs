//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    
    public partial class Provincia
    {
        public Provincia()
        {
            this.Ciudad = new HashSet<Ciudad>();
        }
    
        public int idProvincia { get; set; }
        public string Nombre { get; set; }
        public int idPais { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public Nullable<System.DateTime> FechaBaja { get; set; }
        public Nullable<int> UsuarioBaja { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ciudad> Ciudad { get; set; }
        [JsonIgnore]
        public virtual Pais Pais { get; set; }
    }
}