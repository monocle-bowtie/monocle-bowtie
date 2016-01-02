using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistAdmin.TransferObjects
{
    public class ConfirmarPreventa
    {
        public decimal Monto { get; set; }
        public int idConcepto { get; set; }
        public char TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<PreventasAPagar> PreVentaDetalle { get; set; }
    }
}