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
        private static int ELEM_POR_PAG = 10;

        TextView buttonSiguiente, buttonAnterior;
        ListView listaProd;
        TextView textoPagina;
        List<Producto> listaProductos;
        int indiceIni, indiceFin;
        public string url = "http://beca1/api/producto?idsubcat=";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Productoss);
            var ids = Intent.Extras.GetString("ids");

            //TODO rellenar array
            buttonSiguiente = FindViewById<TextView>(Resource.Id.buttonSiguiente);
            buttonAnterior = FindViewById<TextView>(Resource.Id.buttonAnterior);
            textoPagina = FindViewById<TextView>(Resource.Id.textoPagina);
            listaProd = FindViewById<ListView>(Resource.Id.listaProd);
            listaProductos = new List<Producto>();

            string peticion = url + ids;
            Peticion(peticion);

            buttonAnterior.Click += ButtonAnterior_Click;
            buttonSiguiente.Click += ButtonSiguiente_Click;

            indiceIni = 0;
            indiceFin = ELEM_POR_PAG - 1;
            MostrarRangoProductos();

            listaProd.ItemClick += Vista_itemclick;


        }

        private void ButtonSiguiente_Click(object sender, EventArgs e)
        {
            indiceIni += ELEM_POR_PAG;
            indiceFin += ELEM_POR_PAG;
            MostrarRangoProductos();
        }

        private void ButtonAnterior_Click(object sender, EventArgs e)
        {
            indiceIni -= ELEM_POR_PAG;
            indiceFin -= ELEM_POR_PAG;
            MostrarRangoProductos();
        }

        private void Vista_itemclick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView l = (ListView)sender;
            Producto pro = (Producto)l.GetItemAtPosition(e.Position);

            Intent intent = new Intent(this, typeof(DetalleActivity));
            intent.PutExtra("id", pro.ProductID);

            StartActivity(intent);
        }

        private void MostrarRangoProductos()
        {
            indiceIni = (indiceIni < 0) ? 0 : indiceIni;
            indiceIni = (indiceIni >= listaProductos.Count) ? listaProductos.Count - 1 : indiceIni;
            indiceFin = (indiceFin < 0) ? 0 : indiceFin;
            indiceFin = (indiceFin >= listaProductos.Count) ? listaProductos.Count - 1 : indiceFin;

            Producto[] productos = listaProductos.GetRange(indiceIni, indiceFin - indiceIni + 1).ToArray();
            ProductoItemAdapter adapter = new ProductoItemAdapter(this, productos);
            listaProd.Adapter = adapter;

            buttonAnterior.Enabled = (indiceIni == 0) ? false : true;
            buttonSiguiente.Enabled = (indiceFin == listaProductos.Count - 1) ? false : true;


            textoPagina.Text = (indiceIni + 1).ToString() + " - " + (indiceFin + 1).ToString() + " de " + listaProductos.Count.ToString();

            if (indiceFin == listaProductos.Count - 1)
            {
                indiceFin = (indiceIni + ELEM_POR_PAG - 1);
            }
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