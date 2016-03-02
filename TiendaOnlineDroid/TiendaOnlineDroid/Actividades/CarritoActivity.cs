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
using SQLite;
using System.IO;

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
        private List<Producto> readCarritoDB()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "carrito.db3");
            var db = new SQLiteConnection(dbPath);
            List<Producto> productos = new List<Producto>();
            var table = db.Table<SQProduct>();
            foreach (SQProduct prod in table)
            {
                Producto producto = new Producto();
                producto.ProductID = prod.ProductID;
                producto.Name = prod.Name;
                producto.ProductNumber = prod.ProductNumber;
                producto.StandardCost = prod.StandardCost;
                producto.Color = prod.Color;
                productos.Add(producto);
            }
            return productos;
        }

    }
}