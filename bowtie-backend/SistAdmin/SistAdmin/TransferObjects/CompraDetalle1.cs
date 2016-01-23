using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class CompraDetalle1
    {
        public int idCompra { get; set; }
        public int idCompraDetalle { get; set; }
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoBarras { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public char Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
        
    }
}