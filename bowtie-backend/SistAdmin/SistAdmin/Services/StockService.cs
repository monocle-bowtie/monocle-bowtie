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
    public class StockService : BaseService
    {
        // GET api/StockService
        public List<StockDetalle> getAll()
        {
            List<StockDetalle> sd = new List<StockDetalle>();
            List<Stock> s = this.db.Stock.Where(s1=> s1.Estado=="A").ToList();

            foreach(Stock si in s)
            {
                StockDetalle sdi = new StockDetalle();
                sdi.idStock = si.idStock;
                sdi.idProducto = si.idProducto;
                sdi.FechaAlta = si.FechaAlta;
                sdi.FechaBaja = si.FechaBaja==null?DateTime.Parse("1900-04-12T20:44:55"):(DateTime)si.FechaBaja;
                sdi.UsuarioBaja = si.UsuarioBaja ==null?0:(int)si.UsuarioBaja;
                sdi.Cantidad = si.Cantidad==null?0:(int)si.Cantidad;
                sdi.Estado = si.Estado;
                Producto p = db.Producto.Find(si.idProducto);
                sdi.ProductoDescripcion = p.Nombre;
                sd.Add(sdi);
            }
            return sd;
        }

        // GET api/StockService/5
        public Stock find(long? id)
        {
            return this.db.Stock.Find(id);
        }

        // POST api/StockService
        public Stock saveOrUpdate(Stock s)
        {
            s.Estado = "A";

            if (s.idStock > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = this.db.Stock.Add(s);
            }
            this.save();

            return s;
        }



        // DELETE api/StockService/5
        public void delete(long id)
        {
            Stock s = this.db.Stock.Find(id);
            s.FechaBaja = DateTime.Today;
            s.UsuarioBaja = 1;
            s.Estado = "D";
            db.Entry(s).State = EntityState.Modified;
            //this.db.Stock.Remove(s);
            this.save();
        }
    }
}