using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TiendaOnlineDroid.Models
{
    public class SQProduct
    {
        [PrimaryKey, AutoIncrement]
        public int ProductID { get; set; }

        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
    }
}