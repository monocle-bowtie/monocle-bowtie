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
    public class VentaDetalleService : BaseService
    {
        // GET api/VentaDetalleService
        public List<VentaDetalle> getAll()
        {
            return this.db.VentaDetalle.ToList();
        }

        // GET api/VentaDetalleService/5
        public VentaDetalle find(long? id)
        {
            return this.db.VentaDetalle.Find(id);
        }

        // POST api/VentaDetalleService
        public VentaDetalle saveOrUpdate(VentaDetalle v)
        {
            if (v.idVentaDetalle > 0)
            {
                db.Entry(v).State = EntityState.Modified;
            }
            else
            {
                v = this.db.VentaDetalle.Add(v);
            }
            this.save();

            return v;
        }



        // DELETE api/VentaDetalleService/5
        public void delete(long id)
        {
            VentaDetalle v = this.db.VentaDetalle.Find(id);
            this.db.VentaDetalle.Remove(v);
            this.save();
        }
    }
}