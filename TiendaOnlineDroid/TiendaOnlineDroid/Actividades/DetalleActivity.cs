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
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleProducto);
            List<Producto> lista = cargarDetalle(json);
            JsonValue json = await FechtDetalle(url);

            ProductoAdapter adapter = new ProductoAdapter(lista.ToArray(), this);


        }

        private List<Producto> cargarDetalle(JsonValue json)
        {
            List<Producto> productos = new List<Producto>();
            foreach (JsonValue item in json)
            {
                Producto product = new Producto();
                product.ProductID = item["ProductID"];
                product.Name = item["Name"];
                product.ProductNumber = item["ProductNumber"];
                product.Color = item["Color"];
                product.StandardCost = ["StandardCost"];

                productos.Add(product);
            }
            return productos;
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