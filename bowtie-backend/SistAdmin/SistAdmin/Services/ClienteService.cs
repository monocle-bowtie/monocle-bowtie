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
    public class ClienteService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VendedorService));

        // GET api/Cliente
        public List<Cliente> getAll()
        {
            return this.db.Cliente.Where(v1 => v1.Estado == "A").ToList();
        }

        // GET api/Cliente/5
        public Cliente find(long? id)
        {
            return this.db.Cliente.Find(id);
        }

        // POST api/Cliente
        public Cliente saveOrUpdate(Cliente c)
        {
            if (c.idCliente > 0)
            {
                db.Entry(c).State = EntityState.Modified;
            }
            else
            {
                c = this.db.Cliente.Add(c);
            }
            this.save();

            return c;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/Cliente/5
        public void delete(long id)
        {
            
            Venta v = this.db.Venta.Where(v1 => v1.idCliente == id).FirstOrDefault();
            Cliente c = this.db.Cliente.Find(id);
            if (v == null)
            {

                c.FechaBaja = DateTime.Today;
                c.UsuarioBaja = 1;
                c.Estado = "D";
                db.Entry(c).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                c.Estado = "A";
                db.Entry(c).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}