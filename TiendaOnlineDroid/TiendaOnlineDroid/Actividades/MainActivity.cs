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
using TiendaOnlineDroid.Actividades;

namespace TiendaOnlineDroid
{
    [Activity(Label = "TiendaOnlineDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public string url = "http://beca1/api/";
        public string categoria = "categoria";
        public string subcategoria = "subcategoria?idcat=";
        public string productos = "producto?idsubcat=";
        public List<string> listaNombreCategorias = new List<string>();
        public List<string> listaNombreSubcategorias = new List<string>();
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

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaNombreCategorias.ToArray());

            Spinner spin = FindViewById<Spinner>(Resource.Id.spinnerCategoria);
            spin.Adapter = adapter;
            spin.ItemSelected += spin_ItemSelected;
        }
        private void spin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner s = (Spinner)sender;
            String nombre = (string)s.GetItemAtPosition(e.Position);
            int id=0;

            foreach (Categoria c in listaCategorias)
            {
                if (c.CategoryName == nombre)
                {
                    id = c.CategoryID;
                }
            }
            if (nombre!="Otros")
            {
                string peticion = url + subcategoria+ id.ToString();
                listaNombreSubcategorias.Clear();
                PeticionSubcategoria(peticion);
            }
            else
            {
                listaNombreSubcategorias.Clear();
                listaNombreSubcategorias.Add("Sin subcategorías");
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaNombreSubcategorias.ToArray());
            Spinner spin = FindViewById<Spinner>(Resource.Id.spinnerSubcategoria);
            spin.Adapter = adapter;
            spin.ItemSelected += spinSub_ItemSelected;
        }

        private void spinSub_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner s = (Spinner)sender;
            //String nombre = (string)s.GetItemAtPosition(e.Position);
            //int id = 0;
            //foreach (SubCategoria sc in listaSubcategorias)
            //{
            //    id = sc.SubCategoryID;
            //}

            //Intent intent = new Intent(this, typeof(ProductosActivity));
            //intent.PutExtra("title", pelicula.Title);
            //intent.PutExtra("year", pelicula.Year);
            //intent.PutExtra("imdbID", pelicula.ImdbID);
            //intent.PutExtra("type", pelicula.Type);
            //intent.PutExtra("poster", pelicula.Poster);

            //StartActivity(intent);
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

                        foreach (JsonValue datos in jsonDoc)
                        {
                            Categoria cat = new Categoria();
                            cat.CategoryID = datos["CategoryID"];
                            cat.CategoryName = datos["CategoryName"];
                            listaCategorias.Add(cat);
                            listaNombreCategorias.Add(cat.CategoryName);
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

        private void PeticionSubcategoria(string url)
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
                            SubCategoria subcat = new SubCategoria();
                            subcat.CategoryID = datos["CategoryID"];
                            subcat.SubCategoryID = datos["SubCategoryID"];
                            subcat.SubCategoryName = datos["SubCategoryName"];
                            listaSubcategorias.Add(subcat);
                            listaNombreSubcategorias.Add(subcat.SubCategoryName);
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

