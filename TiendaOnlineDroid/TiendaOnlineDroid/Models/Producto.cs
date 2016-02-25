using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAdventure.Models
{
    public class Producto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandarCost { get; set; }
    }
}