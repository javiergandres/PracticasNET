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
    public class ListaProductosAdapter: BaseAdapter<Producto>
    {
        Producto[] items;
        Activity context;

        public ListaProductosAdapter(Activity context, Producto[] items) : base()
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

                view = context.LayoutInflater.Inflate(Resource.Layout.Productos, null);
            view.FindViewById<TextView>(Resource.Id.NombreProducto).Text = items[position].Name;
            return view;

        }
    }
}