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
    
    public partial class GrupoXProducto
    {
        public int idGrupo { get; set; }
        public int idProducto { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public Nullable<System.DateTime> FechaBaja { get; set; }
        public Nullable<int> UsuarioBaja { get; set; }
        [JsonIgnore]
        public virtual GrupoProducto GrupoProducto { get; set; }
        [JsonIgnore]
        public virtual Producto Producto { get; set; }
    }
}
