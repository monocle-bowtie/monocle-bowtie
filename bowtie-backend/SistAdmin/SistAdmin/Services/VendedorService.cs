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
    public class VendedorService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VendedorService));

        // GET api/Vendedor
        public List<Vendedor> getAll()
        {
            return this.db.Vendedor.Where(v1 => v1.Estado == "A").ToList();
        }

        // GET api/Vendedor/5
        public Vendedor find(long? id)
        {
            return this.db.Vendedor.Find(id);
        }

        // POST api/Vendedor
        public Vendedor saveOrUpdate(Vendedor v)
        {
            if (v.idVendedor > 0)
            {
                db.Entry(v).State = EntityState.Modified;
            }
            else
            {
                v = this.db.Vendedor.Add(v);
            }
            this.save();

            return v;
        }

        // PUT api/<controller>/5
       // public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            Usuario u = this.db.Usuario.Where(u1 => u1.idVendedor == id).FirstOrDefault();
            Venta v = this.db.Venta.Where(v1 => v1.idVendedor == id).FirstOrDefault();
            Vendedor vd = this.db.Vendedor.Find(id);
            if (u == null && v == null)
            {

                vd.FechaBaja = DateTime.Today;
                vd.UsuarioBaja = 1;
                vd.Estado = "D";
                db.Entry(vd).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                vd.Estado = "A";
                db.Entry(vd).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}