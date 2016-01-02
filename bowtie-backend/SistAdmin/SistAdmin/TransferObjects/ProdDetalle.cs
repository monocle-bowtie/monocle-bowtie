using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistAdmin.TransferObjects
{
    public class ProdDetalle
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioLista { get; set; }
        public decimal PrecioContado { get; set; }
        public string CodigoBarras { get; set; }
        public int Stock { get; set; }
    }
}