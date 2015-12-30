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
            Compra C = this.db.Compra.Where(c1 => c1.idProveedor == id).FirstOrDefault();
            ProductoPrecio pp = this.db.ProductoPrecio.Where(pp1 => pp1.idProveedor == id).FirstOrDefault();
            Proveedor p = this.db.Proveedor.Find(id);
            if (C == null && pp == null) 
            { 
                //this.db.Proveedor.Remove(p);
                p.FechaBaja = DateTime.Today;
                p.UsuarioBaja = 1;
                p.Estado = "D";
                db.Entry(p).State = EntityState.Modified;
                this.save();
            }
            else
            {
                p.Estado = "A";
                db.Entry(p).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}