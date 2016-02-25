using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using TiendaOnlineDroid.Models;
using System.Net;
using System.Json;
using System.IO;
using System.Threading.Tasks;

namespace TiendaOnlineDroid
{
    [Activity(Label = "TiendaOnlineDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public string url = "http://beca1/api/";
        public string categoria = "categoria";
        public string subcategoria = "subcategoria?idcat=";
        public string productos = "producto?idsubcat=";
        public List<Categoria> listaCategorias = new List<Categoria>();
        public List<SubCategoria> listaSubcategorias = new List<SubCategoria>();
        public List<Producto> listaProductos = new List<Producto>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            //TODO rellenar array
            string peticion = url + categoria;
            PeticionCategoria(peticion); 

            ArrayAdapter<Categoria> adapter = new ArrayAdapter<Categoria>(this, Android.Resource.Layout.SimpleListItem1, listaCategorias.ToArray());

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
                        JsonValue jsonDoc =  JsonObject.Load(stream);

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
                            Categoria cat = new Categoria();
                            cat.CategoryID = datos["CategoryID"];
                            cat.CategoryName = datos["CategoryName"];
                            listaCategorias.Add(cat);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var aviso = new AlertDialog.Builder(this);
                aviso.SetMessage("Cateoria no encontrada");
                aviso.SetNegativeButton("Aceptar", delegate { });
                aviso.Show();
            }
        }
    }
}

