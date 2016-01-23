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
    public class ProveedorService : BaseService
    {
        // GET api/ProveedorService
        public List<Proveedor> getAll()
        {
            return this.db.Proveedor.Where(p1=>p1.Estado =="A").ToList();
        }

        // GET api/ProveedorService/5
        public Proveedor find(long? id)
        {
            return this.db.Proveedor.Find(id);
        }

        // POST api/ProveedorService
        public Proveedor saveOrUpdate(Proveedor p)
        {
            if (p.idProveedor > 0)
            {
                db.Entry(p).State = EntityState.Modified;
            }
            else
            {
                p = this.db.Proveedor.Add(p);
            }
            this.save();

            return p;
        }



        // DELETE api/ProveedorService/5
        public void delete(long id)
        {
            Proveedor p = this.db.Proveedor.Find(id);
                //this.db.Proveedor.Remove(p);
                p.FechaBaja = DateTime.Today;
                p.UsuarioBaja = 1;
                p.Estado = "D";
                db.Entry(p).State = EntityState.Modified;
                this.save();
         
        }
    }
}