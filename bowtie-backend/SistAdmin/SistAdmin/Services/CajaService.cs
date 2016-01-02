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
    public class CajaService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CajaService));

        // GET api/Caja
        public List<Caja> getAll()
        {
            return this.db.Caja.Where(c1 => c1.Estado == "A").ToList();
        }

        // GET api/Caja/5
        public Caja find(long? id)
        {
            return this.db.Caja.Find(id);
        }

        public List<Caja> getFecha(DateTime p_desde, DateTime p_hasta)
        {
            List<Caja> _caja = this.db.Caja.Where(c1 => c1.FechaAlta >= p_desde && c1.FechaAlta <= p_hasta).ToList();



            return _caja;
        }

        // POST api/<controller>
        public Caja saveOrUpdate(Caja c)
        {
            if (c.idCaja > 0)
            {
                db.Entry(c).State = EntityState.Modified;
            }
            else
            {
                c = this.db.Caja.Add(c);
            }
            this.save();

            return c;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Caja c = this.db.Caja.Find(id);
            c.FechaBaja = DateTime.Today;
            c.UsuarioBaja = 1;
            c.Estado = "D";
            db.Entry(c).State = EntityState.Modified;
            // this.db.Producto.Remove(p);
            this.save();   
        }
    }
}