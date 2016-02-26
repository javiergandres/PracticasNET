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

namespace TiendaOnlineDroid.Actividades
{
    [Activity(Label = "DetalleActivity")]
    public class DetalleActivity : Activity
    {
        public string url = "http://beca1/api/producto?idpro=";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            var id = Intent.Extras.GetString("id");

            url += id;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleProducto);
           
            JsonValue json = await FechtDetalle(url);

            Producto  producto = cargarDetalle(json);

            FindViewById<TextView>(Resource.Id.ProductID).Text = producto.ProductID.ToString();
            FindViewById<TextView>(Resource.Id.Name).Text = producto.Name;
            FindViewById<TextView>(Resource.Id.ProductNumber).Text = producto.ProductNumber;
            FindViewById<TextView>(Resource.Id.Color).Text = producto.Color;
            FindViewById<TextView>(Resource.Id.StandarCost).Text = producto.StandardCost.ToString();

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
    }
}