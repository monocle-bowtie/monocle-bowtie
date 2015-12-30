using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class RegistrarVenta
    {
        public int idVenta { get; set; }
        public int idVendedor { get; set;}
        public int idCliente { get; set; }
        public int idMedioPago { get; set; }
        public int idSucursal { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public decimal totalpromocion { get; set; }
        public char estado { get; set; }
        public DateTime fechaalta { get; set; }
        public int usuarioalta { get; set; }
        public DateTime fechabaja { get; set; }
        public string NroTicket { get; set; }
        public int idTarjeta { get; set; }
        public virtual ICollection<VentaDetalle1> VentaDetalle { get; set; }
        public virtual ICollection<VentaPromoDetalle> VentaPromoDetalle { get; set; }
    }
}