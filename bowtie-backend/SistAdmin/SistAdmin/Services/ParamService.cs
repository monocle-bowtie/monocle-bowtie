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
    public class ParamService : BaseService
    {
        // GET api/Param
        public List<Parametrizacion> getAll()
        {
            return this.db.Parametrizacion.Where(param1 => param1.Estado == "A").ToList();
        }

        // GET api/Param/5
        public Parametrizacion find(long? id)
        {
            return this.db.Parametrizacion.Find(id);
        }

        // POST api/Param
        public Parametrizacion saveOrUpdate(Parametrizacion p)
        {
            if (p.idParametrizacion > 0)
            {
                db.Entry(p).State = EntityState.Modified;
            }
            else
            {
                p = this.db.Parametrizacion.Add(p);
            }
            this.save();

            return p;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {

            Parametrizacion param = new Parametrizacion();
                param.FechaBaja = DateTime.Today;
                param.UsuarioBaja = 1;
                param.Estado = "D";
                db.Entry(param).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            
        }
    }
}