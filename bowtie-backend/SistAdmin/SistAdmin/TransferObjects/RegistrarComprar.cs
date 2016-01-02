using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class RegistrarComprar
    {
        public int idCompra {get; set;}
        public int idProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int idMedioPago { get; set; }
        public char Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
        public string NroFactura { get; set; }
        public int idGrupo { get; set; }
        public virtual ICollection<CompraDetalle1> CompraDetalle { get; set; }
    }
}