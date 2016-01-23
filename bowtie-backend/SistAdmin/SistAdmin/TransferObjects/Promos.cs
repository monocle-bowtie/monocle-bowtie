using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistAdmin.Models;

namespace SistAdmin.TransferObjects
{
    public class Promos
    {
        public int idMedioPago { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}