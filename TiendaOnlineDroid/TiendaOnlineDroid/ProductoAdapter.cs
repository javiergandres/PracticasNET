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

namespace TiendaOnlineDroid
{
    public class ProductoAdapter:BaseAdapter<Producto>
    {
        Producto[] items;
        Activity context;

        public ProductoAdapter(Activity context, Producto[] items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Producto this[int position]
        {
            get
            {
                if (position < items.Length)
                {
                    return items[position];
                }
                return null;
            }
        }

        public override int Count
        {
            get
            {
                return items.Length; ;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)

                view = context.LayoutInflater.Inflate(Resource.Layout.DetalleProducto, null);
            view.FindViewById<TextView>(Resource.Id.ProductID).Text = items[position].ProductID.ToString();
            view.FindViewById<TextView>(Resource.Id.Name).Text = items[position].Name;
            view.FindViewById<TextView>(Resource.Id.ProductNumber).Text = items[position].ProductNumber;
            view.FindViewById<TextView>(Resource.Id.Color).Text = items[position].Color;
            view.FindViewById<TextView>(Resource.Id.StandarCost).Text = items[position].StandardCost.ToString();
            return view;

        }
    }
}
