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
    public class ConceptoService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductoService));

        // GET api/Concepto
        public List<Concepto> getAll()
        {
            return this.db.Concepto.Where(c1 => c1.Estado == "A").ToList();
        }

        // GET api/Concepto/5
        public Concepto find(long? id)
        {
            return this.db.Concepto.Find(id);
        }

        // POST api/Concepto
        public Concepto saveOrUpdate(Concepto c)
        {
            if (c.IdConcepto > 0)
            {
                db.Entry(c).State = EntityState.Modified;
            }
            else
            {
                c = this.db.Concepto.Add(c);
            }
            this.save();

            return c;
        }

        // PUT api/<controller>/5
     //   public void Put(int id, [FromBody]string value)
       // {
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Venta v = this.db.Venta.Where(v1 => v1.idMedioPago == id).FirstOrDefault();
            MedioPago mp = this.db.MedioPago.Find(id);
            if (v == null)
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