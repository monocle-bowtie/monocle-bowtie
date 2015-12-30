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
    public class ProductoService : BaseService
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ProductoService));

        // GET api/Producto
        public List<Producto> getAll()
        {
            return this.db.Producto.Where(p1 => p1.Estado == "A").ToList();
        }

        // GET api/Producto
        public List<productosWithStock> getProdConStock()
        {
            
             return this.db.productosWithStock.ToList();
        }

        // GET api/ProductoService/5
        public Producto find(long? id)
        {
            return this.db.Producto.Find(id);
        }

        // POST api/ProductoService
        public Producto saveOrUpdate(Producto p)
        {
            if (p.idProducto > 0)
            {
                db.Entry(p).State = EntityState.Modified;
            }
            else
            {
                p = this.db.Producto.Add(p);
            }
            this.save();

            return p;
        }

     /*   // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
        */
        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Stock s = this.db.Stock.Where(s1 => s1.idProducto == id).FirstOrDefault();
            CompraDetalle cd = this.db.CompraDetalle.Where(cd1 => cd1.idProducto == id).FirstOrDefault();
            Producto p = this.db.Producto.Find(id);
            if (s == null && cd == null )
            { 
               
                p.FechaBaja = DateTime.Today;
                p.UsuarioBaja = 1;
                p.Estado = "D";
                db.Entry(p).State = EntityState.Modified;
               // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                p.Estado = "A";
                db.Entry(p).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}