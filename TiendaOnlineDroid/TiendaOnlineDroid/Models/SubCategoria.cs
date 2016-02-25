using System;
using System.Collections.Generic;
using System.Linq;


namespace TiendaOnlineDroid.Models
{
    public class SubCategoria : Java.Lang.Object
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }
        public string SubCategoryName { get; set; }
    }
}