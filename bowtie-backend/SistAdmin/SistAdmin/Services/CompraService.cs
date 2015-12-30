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
    public class CompraService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductoService));

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

       

        public void RegistrarCompra(RegistrarComprar r)
        {
            Compra c = new Compra();

            c.idCompra = 0;
            c.idProveedor = r.idProveedor;
            c.Total = r.Total;
            c.UsuarioAlta = 1;
            c.FechaAlta = DateTime.Today;
            c.Fecha = r.Fecha;
            c.NroFactura = r.NroFactura;
            c.idMedioPago = r.idMedioPago;
            c.Estado = "A";

            c = this.db.Compra.Add(c);
            //this.save();
            ///registrar los productos 
            foreach(CompraDetalle1 cd in r.CompraDetalle)
            {
                Producto p = new Producto();
                if (cd.idProducto > 0)
                {
                    //por ahora hard
                    Parametrizacion param = this.db.Parametrizacion.Where(param11 => param11.Descripcion == "PorcentajeMarcador").FirstOrDefault();
                    Parametrizacion param1 = this.db.Parametrizacion.Where(param12 => param12.Descripcion == "PorcentajeContado").FirstOrDefault();
                    Parametrizacion param2 = this.db.Parametrizacion.Where(param13 => param13.Descripcion == "PorcentajeGremio").FirstOrDefault();
                    if (param != null)
                    {
                        decimal porcentaje_lista = (decimal)param.Valor;
                        decimal porcentaje_contado = (decimal)param1.Valor;
                        decimal porcentaje_gremio = (decimal)param2.Valor;
                        decimal PrecioLista = cd.PrecioUnitario * (1 + (porcentaje_lista/100));
                        decimal PrecioContado = PrecioLista - (PrecioLista * porcentaje_contado)/100;
                        decimal PrecioGremio = cd.PrecioUnitario * (1 + (porcentaje_gremio/100));
                        p = this.db.Producto.Find(cd.idProducto);
                        p.PrecioLista = PrecioLista;
                        p.PrecioContado = PrecioContado;
                        p.PrecioGremio = PrecioGremio;
                        p.CodigoBarras = cd.CodigoBarras;
                        db.Entry(p).State = EntityState.Modified;
              //          this.save();
                    }
                    


                    Stock s = this.db.Stock.Where(s1 => s1.idProducto == cd.idProducto)
                        .FirstOrDefault();

                    if (s != null) { 
                    s.Cantidad += cd.Cantidad;
                    db.Entry(s).State = EntityState.Modified;
                //    this.save();
                    }
                    else
                    {
                        Stock s1 = new Stock();
                        s1.idProducto = cd.idProducto;
                        s1.Cantidad  = cd.Cantidad;
                        s1.Estado = "A";
                        s1.FechaAlta = DateTime.Today;
                        s1.UsuarioAlta = 1;
                        s1.idStock = 0;
                        db.Stock.Add(s1);
                  //      this.save();
                    }

                    ProductoPrecio pp = this.db.ProductoPrecio.Where(pp1 => pp1.idProducto == cd.idProducto && pp1.idProveedor == r.idProveedor).FirstOrDefault();
                    if (pp != null) { 
                        pp.PrecioCosto = cd.PrecioUnitario;
                        pp.FechaAlta = DateTime.Today;
                        db.Entry(pp).State = EntityState.Modified;
                    //      this.save();
                    }
                    else
                    {
                        ProductoPrecio pp1 = new ProductoPrecio();
                        pp1.idProducto = cd.idProducto;
                        pp1.idProveedor = r.idProveedor;
                        pp1.PrecioCosto = cd.PrecioUnitario;
                        pp1.Estado = "A";
                        pp1.UsuarioAlta = 1;
                        pp1.FechaAlta = DateTime.Today;     
                        db.ProductoPrecio.Add(pp1);
                      //  this.save();
                    }

                
                }
                else
                {
                    p.idProducto = cd.idProducto;
                    p.Nombre = cd.NombreProducto;
                    p.CodigoBarras = cd.CodigoBarras;
                    p.UsuarioAlta = 1;
                    p.FechaAlta = DateTime.Today;
                    p.Estado = "A";
                    //por ahora hard
                    Parametrizacion param = new Parametrizacion();
                    param = this.db.Parametrizacion.Where(param11 => param11.Descripcion == "PorcentajeMarcador").FirstOrDefault();
                    Parametrizacion param1 = this.db.Parametrizacion.Where(param12 => param12.Descripcion == "PorcentajeContado").FirstOrDefault();
                    Parametrizacion param2 = this.db.Parametrizacion.Where(param13 => param13.Descripcion == "PorcentajeGremio").FirstOrDefault();
                    if (param != null)
                    {
                        decimal porcentaje_contado = (decimal)param1.Valor;
                        decimal porcentaje_lista = (decimal)param.Valor;
                        decimal porcentaje_gremio = (decimal)param2.Valor;

                        decimal PrecioLista = cd.PrecioUnitario * (1 + (porcentaje_lista / 100));
                        decimal PrecioContado = PrecioLista - (PrecioLista * porcentaje_contado) / 100;
                        decimal PrecioGremio = cd.PrecioUnitario * (1 + (porcentaje_gremio / 100));
                        p.PrecioLista = PrecioLista;
                        p.PrecioContado = PrecioContado;
                        p.PrecioGremio = PrecioGremio;
                    }
                    p = this.db.Producto.Add(p);
                    //this.save();

                    Stock s = new Stock();
                    s.Cantidad = cd.Cantidad;
                    s.idProducto = p.idProducto;
                    s.FechaAlta = DateTime.Today;
                    s.UsuarioAlta = 1;
                    s.Estado = "A";
                    s = this.db.Stock.Add(s);
                    //this.save();

                    ProductoPrecio pp = new ProductoPrecio();
                    pp.idProducto = p.idProducto;
                    pp.idProveedor = r.idProveedor;
                    pp.Estado = "A";
                    pp.FechaAlta = DateTime.Today;
                    pp.UsuarioAlta = 1;
                    pp.PrecioCosto = cd.PrecioUnitario;
                    pp = this.db.ProductoPrecio.Add(pp);
                    //this.save();

                    if (r.idGrupo > 0) { 
                        GrupoXProducto gxp = new GrupoXProducto();
                        gxp.idGrupo = r.idGrupo;
                        gxp.idProducto = pp.idProducto;
                        gxp.Estado = "A";
                        gxp.FechaAlta = DateTime.Today;
                        gxp.UsuarioAlta = 1;
                        this.db.GrupoXProducto.Add(gxp);
                      //  this.save();
                    }
                    
                }

                CompraDetalle cp = new CompraDetalle();
                cp.idCompraDetalle = 0;
                cp.idProducto = cd.idProducto == 0?p.idProducto:cd.idProducto;
                cp.idCompra = c.idCompra;
                cp.PrecioUnitario = cd.PrecioUnitario;
                cp.PrecioTotal = cd.PrecioTotal;
                cp.UsuarioAlta = 1;
                cp.FechaAlta = DateTime.Today;
                cp = this.db.CompraDetalle.Add(cp);
                //this.save();
                
            }

            //Registrar Movimiento
            if (r.idMedioPago == 1)
            {
                Caja c3 = new Caja();
                c3.idCaja = 0;
                c3.idConcepto = 2; //Compra
                c3.TipoMovimiento = "E";
                c3.Monto = r.Total;
                c3.Descripcion = "Compra Correspondiente a Nro. Factura: " + r.NroFactura;
                c3.Estado = "A";
                c3.UsuarioAlta = 1;
                c3.FechaAlta = DateTime.Today;
                c3 = this.db.Caja.Add(c3);
                //this.save();
            }

            this.save();
        }

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}