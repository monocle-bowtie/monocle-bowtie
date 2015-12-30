using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class PromoDetalle
    {
        public int idPromocion { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set;}
        public int UsuarioAlta {get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
        public virtual ICollection<PromoProductoDetallev1> PromoProducto { get; set; }
         
    }
}