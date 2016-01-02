using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistAdmin.TransferObjects
{
    public class VentaDetalle1
    {
        public int idVenta { get; set; }
        public int idVentaDetalle { get; set; }
        public int idProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioFinal { get; set; }
        public char Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; } 


    }
}