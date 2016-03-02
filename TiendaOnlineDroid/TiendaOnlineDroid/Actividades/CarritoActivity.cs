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
        List<string> list = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Carross);

            var id = Intent.Extras.GetString("id");
            var name = Intent.Extras.GetString("name");
            var standarCost = Intent.Extras.GetString("standarCost");
            string filaCarrito ="Producto: " +name + ", Precio:  " + standarCost;

            

            list.Add(filaCarrito);
            ListView vista = FindViewById<ListView>(Resource.Id.listCarro);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list.ToArray());
            vista.Adapter = adapter;

            Button botonback = FindViewById<Button>(Resource.Id.backMain);
            botonback.Click += Botonback_Click;

            vista.LongClick += List_LongClick;

        }

        private void List_LongClick(object sender, View.LongClickEventArgs e)
        {
            ListView lista = (ListView)sender;
            Producto producto = (Producto)lista.SelectedItem;
            foreach (string nombre in list)
            {
                if (producto.Name == nombre)
                {
                    var aviso = new AlertDialog.Builder(this);
                    aviso.SetMessage("¿Desea eliminar " + producto.Name + "?");
                    aviso.SetPositiveButton("Aceptar", delegate { list.Remove(nombre);});
                    aviso.SetNegativeButton("Cancelar", delegate { });
                    aviso.Show();
                }
            }
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