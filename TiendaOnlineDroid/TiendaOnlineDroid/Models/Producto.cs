﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace TiendaOnlineDroid.Models
{
    public class Producto : Java.Lang.Object
    {
      
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandarCost { get; set; }
    }
}