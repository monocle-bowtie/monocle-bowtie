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
using SistAdmin.TransferObjects;

namespace SistAdmin.Services
{
    public class GrupoService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GrupoService));

        // GET api/Grupo
        public List<GrupoProducto> getAll()
        {
            return this.db.GrupoProducto.Where(p1 => p1.Estado == "A").ToList();
        }

        public List<GrupoXProducto1> getDetalle()
        {
            List<GrupoXProducto1> resul = new List<GrupoXProducto1>();

            List<GrupoProducto> gp = this.db.GrupoProducto.Where(p1 => p1.Estado == "A").ToList();

            foreach(GrupoProducto _gp in gp)
            {
                GrupoXProducto1 _gp1 = new GrupoXProducto1();

                _gp1.idGrupo = _gp.idGrupo;
                _gp1.PorcentajeGrupo = _gp.PorcentajeGrupo == null ? 0:(decimal)_gp.PorcentajeGrupo;
                _gp1.Descripcion = _gp.Descripcion;
                _gp1.FechaAlta = _gp.FechaAlta;
                _gp1.UsuarioAlta = _gp.UsuarioAlta;
                _gp1.Estado = _gp.Estado;
                _gp1.ProductoDetalle = this.db.GrupoXProducto.Where(gxp1 => gxp1.idGrupo == _gp.idGrupo).ToList();
                resul.Add(_gp1);

            }


            return resul;

        }

        // GET api/Grupo/5
        public GrupoProducto find(long? id)
        {
            return this.db.GrupoProducto.Find(id);
        }

        // POST api/Grupo
        public GrupoProducto saveOrUpdate(GrupoProducto gp)
        {
            if (gp.idGrupo > 0)
            {
                db.Entry(gp).State = EntityState.Modified;
            }
            else
            {
                gp = this.db.GrupoProducto.Add(gp);
            }
            this.save();

            return gp;
        }

        // POST api/Grupo
        public GrupoXProducto1 saveOrUpdate1(GrupoXProducto1  gp)
        {
            if (gp.idGrupo > 0)
            {
                GrupoProducto _gp = this.db.GrupoProducto.Where(gp1 => gp1.idGrupo == gp.idGrupo).FirstOrDefault();
                db.Entry(_gp).State = EntityState.Modified;
                foreach(GrupoXProducto _gxp in gp.ProductoDetalle)
                {
                    GrupoXProducto _gxp1 = new GrupoXProducto();
                    _gxp1.idGrupo = gp.idGrupo;
                    _gxp1.idProducto = _gxp.idProducto;
                    _gxp1.Estado = "A";
                    _gxp1.FechaAlta = DateTime.Today;
                    _gxp1.UsuarioAlta = 1;
                    this.db.GrupoXProducto.Add(_gxp1);
                }
            }
            else
            {
                GrupoProducto _gp = new GrupoProducto();
                _gp.idGrupo = 0;
                _gp.Descripcion = gp.Descripcion;
                _gp.PorcentajeGrupo = gp.PorcentajeGrupo;
                _gp.FechaAlta = DateTime.Today;
                _gp.UsuarioAlta = 1;
                _gp.Estado = "A";
                _gp = this.db.GrupoProducto.Add(_gp);
                this.save();

                foreach (GrupoXProducto _gxp in gp.ProductoDetalle)
                {
                    GrupoXProducto _gxp1 = new GrupoXProducto();
                    _gxp1.idGrupo = _gp.idGrupo;
                    _gxp1.idProducto = _gxp.idProducto;
                    _gxp1.Estado = "A";
                    _gxp1.FechaAlta = DateTime.Today;
                    _gxp1.UsuarioAlta = 1;
                    this.db.GrupoXProducto.Add(_gxp1);
                }

            }

            this.save();

            return gp;
        }

        // PUT api/<controller>/5
      //  public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void delete(long id)
        {
            GrupoXProducto gxp = this.db.GrupoXProducto.Where(gxp1 => gxp1.idGrupo == id).FirstOrDefault();
            GrupoProducto gp = this.db.GrupoProducto.Find(id);
            if (gxp == null)
            {

                gxp.FechaBaja = DateTime.Today;
                gxp.UsuarioBaja = 1;
                gxp.Estado = "D";
                db.Entry(gxp).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
            else
            {
                gxp.Estado = "A";
                db.Entry(gxp).State = EntityState.Modified;
                // this.db.Producto.Remove(p);
                this.save();
            }
        }
    }
}