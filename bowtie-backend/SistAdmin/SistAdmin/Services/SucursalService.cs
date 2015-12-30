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

namespace SistAdmin.Services
{
    public class SucursalService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SucursalService));

        // GET api/Sucursal
        public List<Sucursal> getAll()
        {
            return this.db.Sucursal.Where(s1 => s1.Estado == "A").ToList();
        }

        // GET api/Sucursal/5
        public Sucursal find(long? id)
        {
            return this.db.Sucursal.Find(id);
        }

        // POST api/Sucursal
        public Sucursal saveOrUpdate(Sucursal s)
        {
            if (s.idSucursal > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = this.db.Sucursal.Add(s);
            }
            this.save();

            return s;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Venta v = this.db.Venta.Where(v1 => v1.idSucursal == id).FirstOrDefault();
            Sucursal s = this.db.Sucursal.Find(id);
            if (v == null)
            {

                s.FechaBaja = DateTime.Today;
                s.UsuarioBaja = 1;
                s.Estado = "D";
                db.Entry(s).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                s.Estado = "A";
                db.Entry(s).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}