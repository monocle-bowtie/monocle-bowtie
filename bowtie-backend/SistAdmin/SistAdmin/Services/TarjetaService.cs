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
    public class TarjetaService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CajaService));


        // GET api/<controller>
        public List<Tarjeta> getAll()
        {
            return this.db.Tarjeta.Where(t1 => t1.Estado == "A").ToList();
        }

        // GET api/<controller>/5
        public Tarjeta find(long? id)
        {
            return this.db.Tarjeta.Find(id);
        }

        // POST api/<controller>
        public Tarjeta saveOrUpdate(Tarjeta c)
        {
            if (c.idTarjeta > 0)
            {
                db.Entry(c).State = EntityState.Modified;
            }
            else
            {
                c = this.db.Tarjeta.Add(c);
            }
            this.save();

            return c;
        }

        // PUT api/<controller>/5
       // public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Venta vta = this.db.Venta.Where(vta1 => vta1.idTarjeta == id).FirstOrDefault();
            Tarjeta t = this.db.Tarjeta.Find(id);

            if (vta == null)
            { 
                t.FechaBaja = DateTime.Today;
                t.UsuarioBaja = 1;
                t.Estado = "D";
                db.Entry(t).State = EntityState.Modified;
            }
            else
            {
                t.Estado = "A";
            }
            // this.db.Producto.Remove(p);
            this.save();
        }
    }
}