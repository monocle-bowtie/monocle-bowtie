using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class GrupoXProducto1
    {
        public int idGrupo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
        public decimal PorcentajeGrupo { get; set; }
        public virtual ICollection<GrupoXProducto> ProductoDetalle { get; set; }
    }
}