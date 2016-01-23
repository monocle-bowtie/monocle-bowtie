using SistAdmin.Models;
using log4net;
using Mastercard.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SistAdmin.TransferObjects;

namespace SistAdmin.Services
{
    public class PromocionService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PromocionService));

        // GET api/Promocion
        public List<Promocion> getAll()
        {
            return this.db.Promocion.Where(p1 => p1.Estado == "A").ToList();
        }

        // GET api/Promocion/5
        public Promocion find(long? id)
        {
            return this.db.Promocion.Find(id);
        }

        public List<PromoDetalle> GetPromoDetalle()
        {
            List<PromoDetalle> Resul = new List<PromoDetalle>();
            List<Promocion> p = this.db.Promocion.Where(p1 => p1.Estado == "A").ToList();

            foreach (Promocion Reg in p) {
                    PromoDetalle pd = new PromoDetalle();
                    pd.idPromocion = Reg.IdPromocion;
                    pd.Descripcion = Reg.Descripcion;
                    pd.PrecioContado = Reg.PrecioContado == null ? 0 : (decimal)Reg.PrecioContado;
                    pd.PrecioTarjeta = Reg.PrecioTarjeta == null ? 0 : (decimal)Reg.PrecioTarjeta;
                    pd.Estado = Reg.Estado;
                    pd.FechaAlta = Reg.FechaAlta;
                    pd.UsuarioAlta = Reg.UsuarioAlta;
                    //pd.FechaBaja = p.FechaBaja;
                    //pd.PromoProducto = 
                    List<PromocionProducto> _p1 = this.db.PromocionProducto.Where(pp1 => pp1.idPromocion == Reg.IdPromocion).ToList();
                    pd.PromoProducto = new List<PromoProductoDetallev1>();

                    foreach (PromocionProducto _Re in _p1)
                    {
                        PromoProductoDetallev1 _r = new PromoProductoDetallev1();
                        _r.idPromocion = _Re.idPromocion;
                        _r.idProducto = _Re.idProducto;
                        _r.Descripcion = this.db.Producto.Find(_r.idProducto).Nombre;
                        pd.PromoProducto.Add(_r);
                        
                    }
                    Resul.Add(pd);
            }
            return Resul;
        }

        public List<Promocion> GetPromos(Promos pLista)
        {
            List<Promocion> Resul = new List<Promocion>();
            List<Promocion> ppLista = this.db.Promocion.ToList();
            List<Producto> pListav2 = new List<Producto>();

            foreach(Producto pr1 in pLista.Productos)
            {
                Producto pr2 = this.db.Producto.Find(pr1.idProducto);
                pListav2.Add(pr2);
            }

            bool aplica = true;
            decimal sumaProductos = 0;
            decimal precioProducto = 0;



            foreach (Promocion _p in ppLista)
            {
                foreach (PromocionProducto _pp in _p.PromocionProducto)
                {
                    bool existe = false;
                    foreach (Producto _prod in pListav2)
                    {
                        if (_prod.idProducto == _pp.idProducto)
                        {
                            existe = true;
                            if (pLista.idMedioPago == 1)
                                precioProducto = _prod.PrecioContado==null?0:(decimal)_prod.PrecioContado;
                            else
                                precioProducto = _prod.PrecioLista==null?0:(decimal)_prod.PrecioLista;
                            sumaProductos += precioProducto;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        aplica = false;
                        break;
                    }
                }

                if (aplica)
                {
                    Promocion prod = this.db.Promocion.Find(_p.IdPromocion);
                    if (pLista.idMedioPago == 1)
                        prod.PrecioContado = sumaProductos - prod.PrecioContado;
                    else
                        prod.PrecioTarjeta = sumaProductos - prod.PrecioTarjeta;
                    Resul.Add(prod);
                }
            }
            
            
            return Resul;
        }

        // POST api/<controller>
        public Promocion saveOrUpdate(Promocion p)
        {
            if (p.IdPromocion > 0)
            {
                db.Entry(p).State = EntityState.Modified;
            }
            else
            {
                p = this.db.Promocion.Add(p);
            }
            this.save();

            return p;
        }

        public PromocionProducto SaveDetalle(PromocionProducto p)
        {
            
                p = this.db.PromocionProducto.Add(p);
            
            this.save();

            return p;
        }

        public PromoDetalle SaveDetalleV2(PromoDetalle p)
        {
            if (p.idPromocion>0)
            {
                foreach (PromoProductoDetallev1 pp in p.PromoProducto)
                {
                    PromocionProducto _pp = new PromocionProducto();
                    _pp.idProducto = pp.idProducto;
                    _pp.idPromocion = p.idPromocion;
                    _pp.UsuarioAlta = 1;
                    _pp.FechaAlta = DateTime.Today;
                    _pp.Estado = "A";
                    this.db.PromocionProducto.Add(_pp);
                }
                this.save();
            }
            else
            {
                Promocion _p = new Promocion();
                _p.IdPromocion = 0;
                _p.PrecioContado = p.PrecioContado;
                _p.PrecioTarjeta = p.PrecioTarjeta;
                _p.UsuarioAlta = p.UsuarioAlta;
                _p.FechaAlta = p.FechaAlta;
                _p.Descripcion = p.Descripcion;
                _p.Estado = "A";
                _p = this.db.Promocion.Add(_p);
                this.save();

                foreach(PromoProductoDetallev1 pp in p.PromoProducto)
                {
                    PromocionProducto _pp = new PromocionProducto();
                    _pp.idProducto = pp.idProducto;
                    _pp.idPromocion = _p.IdPromocion;
                    _pp.UsuarioAlta = 1;
                    _pp.FechaAlta = DateTime.Today;
                    _pp.Estado = "A";
                    this.db.PromocionProducto.Add(_pp);
                    this.save();
                }
                
            }


            return p;
        }

        // PUT api/<controller>/5
      //  public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            
            
            Promocion p = this.db.Promocion.Find(id);
            
                p.FechaBaja = DateTime.Today;
                p.UsuarioBaja = 1;
                p.Estado = "D";
                db.Entry(p).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            
        }
    }
}