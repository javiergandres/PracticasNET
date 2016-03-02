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
        List<Producto> listProductos = new List<Producto>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Carross);

            //var id = Intent.Extras.GetString("id");
            //var name = Intent.Extras.GetString("name");
            //var standarCost = Intent.Extras.GetString("standarCost");
            //string filaCarrito ="Producto: " +name + ", Precio:  " + standarCost;

            listProductos = readCarritoDB();

            //listProductos.Add(filaCarrito);
            ListView vista = FindViewById<ListView>(Resource.Id.listCarro);
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list.ToArray());
            ProductoItemAdapter adapter = new ProductoItemAdapter(this, listProductos.ToArray());
            vista.Adapter = adapter;

            Button botonback = FindViewById<Button>(Resource.Id.backMain);
            botonback.Click += Botonback_Click;

            vista.ItemLongClick += Vista_ItemLongClick;

        }

        private void Vista_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            ListView lista = (ListView)sender;
            Producto producto = (Producto)lista.GetItemAtPosition(e.Position);
            foreach (Producto p in listProductos)
            {
                if (producto == p)
                {
                    var aviso = new AlertDialog.Builder(this);
                    aviso.SetMessage("¿Desea eliminar " + producto.Name + "?");
                    aviso.SetPositiveButton("Aceptar", delegate {
                        deleteProductDb(p);
                        listProductos = readCarritoDB();
                        ProductoItemAdapter adapter = new ProductoItemAdapter(this, listProductos.ToArray());
                        lista.Adapter = adapter;
                    });
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
                producto.Cantidad = prod.Cantidad;
                productos.Add(producto);
            }
            return productos;
        }

        private void deleteProductDb(Producto producto)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "carrito.db3");
            SQProduct productosq = new SQProduct();
            
            var db = new SQLiteConnection(dbPath);
            db.Delete<SQProduct>(producto.ProductID);
            listProductos.Remove(producto);
        }

    }
}