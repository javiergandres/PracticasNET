using System;
using System.Collections.Generic;
using System.Linq;


namespace WebApiAdventure.Models
{
    public class Producto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
    }
}