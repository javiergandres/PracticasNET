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

            SetContentView(Resource.Layout.Main);

            //TODO rellenar array
            string peticion = url;
            PeticionCategoria(peticion);

            ArrayAdapter<Producto> adapter = new ArrayAdapter<Producto>(this, Android.Resource.Layout.SimpleListItem1, listaProductos.ToArray());

            Spinner spin = FindViewById<Spinner>(Resource.Id.spinnerCategoria);
            spin.Adapter = adapter;
            spin.ItemSelected += spin_ItemSelected;
        }
        private void spin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }
        private void PeticionCategoria(string url)
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

                        //var datos = jsonDoc["Search"];
                        //arrayCategorias = new Categoria[datos.Count];
                        //for (int i = 0; i < datos.Count; i++)
                        //{
                        //    Categoria cat = new Categoria();
                        //    if (cat.CategoryID != 0)
                        //    {
                        //        cat.CategoryID = datos[i]["CategoryID"];
                        //        cat.CategoryName = datos[i]["CategoryName"];
                        //        arrayCategorias[i] = cat;
                        //    }
                        //}
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
                aviso.SetMessage("Categoria no encontrada");
                aviso.SetNegativeButton("Aceptar", delegate { });
                aviso.Show();
            }
        }
    }
}