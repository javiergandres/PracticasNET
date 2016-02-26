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
using TiendaOnlineDroid.Models;

namespace TiendaOnlineDroid.Actividades
{
    [Activity(Label = "DetalleActivity")]
    public class DetalleActivity : Activity
    {
        public string url = "http://beca1/api/producto?idpro=";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleProducto);

            GetDetalle(url);
            
        }

        private void GetDetalle(string url)
        {
            throw new NotImplementedException();
        }
    }
}