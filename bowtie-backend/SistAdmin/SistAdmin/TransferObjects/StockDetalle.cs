using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistAdmin.TransferObjects
{
    public class StockDetalle
    {
        public int idStock { get; set; }
        public int idProducto { get; set; }
        public string ProductoDescripcion { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
    }
}