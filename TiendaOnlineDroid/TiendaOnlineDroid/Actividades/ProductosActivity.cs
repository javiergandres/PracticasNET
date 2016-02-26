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
using System.Json;
using System.IO;
using TiendaOnlineDroid.Models;
using System.Net;


namespace TiendaOnlineDroid.Actividades
{
    [Activity(Label = "ProductosActivity")]
    public class ProductosActivity : Activity
    {
        public string url = "http://beca1/api/producto?idsubcat=";
        public List<Producto> listaProductos = new List<Producto>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Productoss);
            var ids = Intent.Extras.GetString("ids");

            //TODO rellenar array
            string peticion = url + ids;
            Peticion(peticion);

            ListView vista = FindViewById<ListView>(Resource.Id.listProducto);
            ListaProductosAdapter adapter = new ListaProductosAdapter(this, listaProductos.ToArray());
            vista.Adapter = adapter;

            vista.ItemClick += Vista_itemclick;
            
       
        }

        private void Vista_itemclick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView l = (ListView)sender;
            Producto pro = (Producto)l.GetItemAtPosition(e.Position);
            string nombre = pro.Name;
            Producto product = new Producto();
            foreach (Producto p in listaProductos)
            {
                if (p.Name == nombre)
                {
                    product = p;
                }
            }
            Intent intent = new Intent(this, typeof(DetalleActivity));
            Producto env = product;
            intent.PutExtra("nombre", env.Name);
            intent.PutExtra("precio", env.StandardCost.ToString());
            intent.PutExtra("id", env.ProductID);


            StartActivity(intent);
        }

        private void Peticion(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        JsonValue jsonDoc = JsonObject.Load(stream);

                       
                        foreach (JsonValue datos in jsonDoc)
                        {
                            Producto prod = new Producto();
                            prod.ProductID = datos["ProductID"];
                            prod.Name = datos["Name"];
                            prod.ProductNumber = datos["ProductNumber"];
                            prod.Color = datos["Color"];
                            prod.StandardCost = datos["StandardCost"];
                            listaProductos.Add(prod);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var aviso = new AlertDialog.Builder(this);
                aviso.SetMessage("Producto no encontrado");
                aviso.SetNegativeButton("Aceptar", delegate { });
                aviso.Show();
            }
        }
    }
}