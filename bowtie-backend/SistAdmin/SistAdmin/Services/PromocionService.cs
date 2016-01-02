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
                    pd.Precio = Reg.Precio == null ? 0 : (decimal)Reg.Precio;
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
                _p.Precio = p.Precio;
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