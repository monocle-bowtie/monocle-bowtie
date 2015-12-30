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
    public class MedioPagoService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MedioPagoService));

        // GET api/MedioPago
        public List<MedioPago> getAll()
        {
            return this.db.MedioPago.Where(mp1 => mp1.Estado == "A").ToList();
        }

        // GET api/<controller>/5
        public MedioPago find(long? id)
        {
            return this.db.MedioPago.Find(id);
        }

        // POST api/MedioPago
        public MedioPago saveOrUpdate(MedioPago mp)
        {
            if (mp.idMedioPago > 0)
            {
                db.Entry(mp).State = EntityState.Modified;
            }
            else
            {
                mp = this.db.MedioPago.Add(mp);
            }
            this.save();

            return mp;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Venta v = this.db.Venta.Where(v1 => v1.idMedioPago == id).FirstOrDefault();
            Compra c = this.db.Compra.Where(c1 => c1.idMedioPago == id).FirstOrDefault();
            MedioPago mp = this.db.MedioPago.Find(id);
            if (v == null && c == null)
            {

                mp.FechaBaja = DateTime.Today;
                mp.UsuarioBaja = 1;
                mp.Estado = "D";
                db.Entry(mp).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                mp.Estado = "A";
                db.Entry(mp).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}