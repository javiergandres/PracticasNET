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
using System.Threading.Tasks;
using System.Net;
using System.IO;
using TiendaOnlineDroid.Models;
using SQLite;

namespace TiendaOnlineDroid.Actividades
{
    [Activity(Label = "DetalleActivity")]
    public class DetalleActivity : Activity
    {
        public string url = "http://beca1/api/producto?idpro=";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            var id = Intent.Extras.GetInt("id");

            url += id;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleProducto);
           
            JsonValue json = await FechtDetalle(url);

            Producto  producto = cargarDetalle(json);
            InsertProductDb(producto);

            
            FindViewById<TextView>(Resource.Id.ProductID).Text = producto.ProductID.ToString();
            FindViewById<TextView>(Resource.Id.Name).Text = producto.Name;
            FindViewById<TextView>(Resource.Id.ProductNumber).Text = producto.ProductNumber;
            FindViewById<TextView>(Resource.Id.Color).Text = producto.Color;
            FindViewById<TextView>(Resource.Id.StandarCost).Text = producto.StandardCost.ToString();

            Button boton = FindViewById<Button>(Resource.Id.buttonDetalle);
            boton.Click += Boton_Click;

        }

        private void Boton_Click(object sender, EventArgs e)
        {

            Intent intent = new Intent(this, typeof(CarritoActivity));

            string ID = FindViewById<TextView>(Resource.Id.ProductID).Text;
            intent.PutExtra("id", ID);
            intent.PutExtra("name", FindViewById<TextView>(Resource.Id.Name).Text);
            intent.PutExtra("productNumber", FindViewById<TextView>(Resource.Id.ProductNumber).Text);
            intent.PutExtra("color", FindViewById<TextView>(Resource.Id.Color).Text);
            string Cost = FindViewById<TextView>(Resource.Id.StandarCost).Text;
            intent.PutExtra("standarCost", Cost);

            StartActivity(intent);
        }

        private Producto cargarDetalle(JsonValue json)
        {
            Producto producto = new Producto();
                        
                Producto product = new Producto();
                product.ProductID = json["ProductID"];
                product.Name = json["Name"];
                product.ProductNumber = json["ProductNumber"];
                product.Color = json["Color"];
                product.StandardCost = json["StandardCost"];              
            
            return product;
        }



        private async Task<JsonValue> FechtDetalle(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:

                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
        public void InsertProductDb(Producto producto)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "carrito.db3");
            var db = new SQLiteConnection(dbPath);

            SQProduct prod = new SQProduct();
            prod.Name = producto.Name;
            prod.ProductID = producto.ProductID;
            prod.StandardCost = producto.StandardCost;
            prod.Color = producto.Color;
            prod.ProductNumber = producto.ProductNumber;    
                          
            db.Insert(prod);
                     
        }
    }
}