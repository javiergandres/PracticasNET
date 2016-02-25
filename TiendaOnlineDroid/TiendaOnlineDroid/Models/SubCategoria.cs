using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAdventure.Models
{
    public class SubCategoria
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }
        public string SubCategoryName { get; set; }
    }
}