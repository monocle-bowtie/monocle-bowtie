using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistAdmin.TransferObjects
{
    public class Preventa
    {
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}