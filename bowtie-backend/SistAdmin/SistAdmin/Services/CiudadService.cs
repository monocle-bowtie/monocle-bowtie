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
    public class CiudadService : BaseService
    {
        // GET api/Ciudad
        public List<Ciudad> getAll()
        {
            return this.db.Ciudad.ToList();
        }


        // GET api/Ciudad/5
        public Ciudad find(long? id)
        {
            return this.db.Ciudad.Find(id);
        }

        // POST api/<controller>
        public Ciudad saveOrUpdate(Ciudad c)
        {
            if (c.idCiudad > 0)
            {
                db.Entry(c).State = EntityState.Modified;
            }
            else
            {
                c = this.db.Ciudad.Add(c);
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
            Ciudad c = this.db.Ciudad.Find(id);
            this.db.Ciudad.Remove(c);
            this.save();
        }
    }
}