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

namespace TiendaOnlineDroid.Actividades
{
    [Activity(Label = "CarritoActivity")]
    public class CarritoActivity : Activity
    {
        List<string> lista = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Carross);

            var id = Intent.Extras.GetString("id");
            var name = Intent.Extras.GetString("name");
            var standarCost = Intent.Extras.GetString("standarCost");
            string filaCarrito ="Producto: " +name + ", Precio:  " + standarCost;

            

            lista.Add(filaCarrito);
            ListView vista = FindViewById<ListView>(Resource.Id.listCarro);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lista.ToArray());
            vista.Adapter = adapter;

            Button botonback = FindViewById<Button>(Resource.Id.backMain);
            botonback.Click += Botonback_Click;

        }

        private void Botonback_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

    }
}