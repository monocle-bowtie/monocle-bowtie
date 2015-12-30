using SistAdmin.Models;
using SistAdmin.TransferObjects;
using log4net;
using Mastercard.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistAdmin.Services
{
    public class VentaService : BaseService
    {
        // GET api/VentaService
        public List<Venta> getAll()
        {
            return this.db.Venta.Where(v1=> v1.Estado == "A").ToList();
        }

        public List<Preventa> getPreVentas()
        {
            int idMedioPago;
            List<Preventa> _P = new List<Preventa>();


            MedioPago mp = this.db.MedioPago.Where(mp1 => mp1.Descripcion == "PreVenta").FirstOrDefault();
            if (mp != null)
            {
                idMedioPago = mp.idMedioPago;
            }
            else
            {
                idMedioPago = 0;
            }

            List<Venta> _v = this.db.Venta.Where(v1 => v1.Estado == "A" && v1.idMedioPago == idMedioPago).ToList();
            
            foreach (Venta v1 in _v)
            {
                Preventa prt = new Preventa();
                prt.idVenta = v1.idVenta;
                prt.idCliente = v1.idCliente;
                prt.Fecha = v1.Fecha;
                prt.Total = (decimal)v1.Total - (v1.MontoPagado==null?0:(decimal)v1.MontoPagado);
                _P.Add(prt);
            }
            return _P;
        }

        // GET api/VentaService/5
        public Venta find(long? id)
        {
            return this.db.Venta.Find(id);
        }

        // POST api/VentaService
        public Venta saveOrUpdate(Venta v)
        {
            if (v.idVenta > 0)
            {
                db.Entry(v).State = EntityState.Modified;
            }
            else
            {
                v = this.db.Venta.Add(v);
            }
            this.save();

            return v;
        }



        // DELETE api/VentaService/5
        public void delete(long id)
        {
            Venta v = this.db.Venta.Find(id);
            this.db.Venta.Remove(v);
            this.save();
        }

        public void RegistrarPreventa(ConfirmarPreventa CP)
        {
            Caja C = new Caja();
            C.idCaja = 0;
            C.idConcepto = CP.idConcepto;
            C.Monto = CP.Monto;
            C.TipoMovimiento = CP.TipoMovimiento == null ? "I":(string)CP.TipoMovimiento.ToString();
            C.UsuarioAlta = 1;
            C.Descripcion = CP.Descripcion;
            C.FechaAlta = DateTime.Today;
            C.Estado = "A";
            this.db.Caja.Add(C);
            this.save();

            foreach (PreventasAPagar PAP in CP.PreVentaDetalle)
            {
                Venta _v = this.db.Venta.Where(v1 => v1.idVenta == PAP.idVenta).FirstOrDefault();

                if (_v != null)
                {
                    if ( _v.Total <= CP.Monto)
                    {
                        MedioPago mp = this.db.MedioPago.Where(mp1 => mp1.Descripcion == "Efectivo").FirstOrDefault();

                        if (mp != null)
                        {
                            _v.idMedioPago = mp.idMedioPago;
                            db.Entry(_v).State = EntityState.Modified;
                            this.save();
                        }
                        CP.Monto = (decimal)CP.Monto - (decimal)_v.Total;
                    }
                    else
                    {
                        if (CP.Monto > 0)
                        {
                            _v.MontoPagado = CP.Monto;
                            db.Entry(_v).State = EntityState.Modified;
                            this.save();
                        }
                    }
                    
                }
            }

          
        }

        public void RegistrarVenta (RegistrarVenta RV)
        {
            Venta v = new Venta();
            v.idVenta = RV.idVenta;
            v.idVendedor = RV.idVendedor;
            v.idCliente = RV.idCliente;
            v.Fecha = RV.fecha;
            v.Total = RV.total;
            v.Estado = "A";
            RV.estado = 'A';
            v.FechaAlta = DateTime.Today;
            v.UsuarioAlta = 1;
            v.idMedioPago = RV.idMedioPago;
            v.idSucursal = RV.idSucursal;
            v.NroTicket = RV.NroTicket;
            v.idTarjeta = RV.idTarjeta;
            v.TotalPromocion = RV.totalpromocion;
            v = this.db.Venta.Add(v);
            RV.idVenta = v.idVenta;
          //  this.save();
            foreach (VentaDetalle1 VD in RV.VentaDetalle )
            {
                VentaDetalle v_vd = new VentaDetalle();
                v_vd.idVenta = v.idVenta;
                v_vd.idVentaDetalle = VD.idVentaDetalle;
                v_vd.idProducto = VD.idProducto;
                v_vd.PrecioUnitario = VD.PrecioUnitario;
                v_vd.PrecioFinal = VD.PrecioFinal;
                v_vd.Cantidad = VD.Cantidad;
                v_vd.FechaAlta = DateTime.Today;
                v_vd.Estado = "A";
                v_vd.UsuarioAlta = 1;
                v_vd = this.db.VentaDetalle.Add(v_vd);
                VD.idVentaDetalle = v_vd.idVentaDetalle;   
 
                
                
                //Descontar el Stock
                Stock s = this.db.Stock.Where(s1 => s1.idProducto == VD.idProducto).FirstOrDefault();
                if (s != null)
                {
                    s.Cantidad = s.Cantidad - VD.Cantidad;
                    db.Entry(s).State = EntityState.Modified;
                    
                }
            }
            foreach (VentaPromoDetalle VP in RV.VentaPromoDetalle)
            {
                VentaPromoDetalle v_vpd = new VentaPromoDetalle();
                v_vpd.IdVenta = v.idVenta;
                v_vpd.idPromocion = VP.idPromocion;
                v_vpd.idPromoDetalle = VP.idPromoDetalle;
                v_vpd.Cantidad = VP.Cantidad;
                v_vpd.PrecioUnitario = VP.PrecioUnitario;
                v_vpd.PrecioTotal = VP.PrecioTotal;
                v_vpd.Estado = "A";
                v_vpd.UsuarioAlta = 1;
                v_vpd.FechaAlta = DateTime.Today;
                v_vpd = this.db.VentaPromoDetalle.Add(v_vpd);
                VP.idPromoDetalle = v_vpd.idPromoDetalle;
                
            }
            //Impactar la caja dependiendo del medio de pago (1 - Efectivo)
            if (RV.idMedioPago == 1)
            {
                Caja c = new Caja();
                c.idCaja = 0;
                c.idConcepto = this.db.Concepto.Where(c1 => c1.Descripcion == "Venta").FirstOrDefault().IdConcepto;
                c.Descripcion = "Venta Correspondiente a la Venta nro: " + RV.NroTicket;
                c.TipoMovimiento = "I";
                c.Monto = RV.total;
                c.Estado = "A";
                c.FechaAlta = DateTime.Today;
                c.UsuarioAlta = 1;
                c = this.db.Caja.Add(c);

                
            }
            this.save();
        }
    }
}