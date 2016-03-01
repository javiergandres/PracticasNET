using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiAdventure.AdventureWork;
using WebApiAdventure.Models;

namespace WebApiAdventure.Controllers
{
    public class ProductoController : ApiController
    {
        private AdWorksEntities db = new AdWorksEntities();

        // GET: api/Producto/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProduct(int idsubcat)
        {
            int? idSubCategoria;
            List<Product> listaProduct;
            List<Producto> listaDeProductos = new List<Producto>();

            if (idsubcat == 0)
            {
                idSubCategoria = null;
            }
            else
            {
                idSubCategoria = idsubcat;
            }

            listaProduct = (from producto in db.Product
                            where producto.ProductSubcategoryID == idSubCategoria
                            select producto).ToList();

            foreach(Product producto in listaProduct)
            {
                Producto p = new Producto();
                p.ProductID = producto.ProductID;
                p.Name = producto.Name;
                p.ProductNumber = producto.ProductNumber;
                p.Color = producto.Color;
                p.StandardCost = producto.StandardCost;
                listaDeProductos.Add(p);
            }


            if(listaDeProductos.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listaDeProductos);
            }
        }
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProductDetail(int idpro)
        {
            var product = (from pro in db.Product
                          join proproph in db.ProductProductPhoto on pro.ProductID equals proproph.ProductID
                          join proph in db.ProductPhoto on proproph.ProductPhotoID equals proph.ProductPhotoID
                          where pro.ProductID == idpro                         
                          select new {pro, proph.LargePhoto,proph.LargePhotoFileName}).FirstOrDefault();

            Producto producto = new Producto();
            producto.ProductID = product.pro.ProductID;
            producto.Name = product.pro.Name;
            producto.Color = product.pro.Color;
            producto.StandardCost = Decimal.Round(product.pro.StandardCost,2);
            producto.ProductNumber = product.pro.ProductNumber;
            producto.ImageUrl = "http:\\\\BECA1\\Images\\" + product.LargePhotoFileName;
            SaveImage(product.LargePhoto, product.LargePhotoFileName);

            if (product==null)
            {
                return NotFound();
            }

            return Ok(producto);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public void SaveImage (Byte[] ImgBytes, string ImageName)

        {

            Bitmap imagen = null;

            Byte[] bytes = (Byte[])(ImgBytes);

            MemoryStream ms = new MemoryStream(bytes);

            imagen = new Bitmap(ms);

            string path = "C:\\inetpub\\wwwroot\\Images\\";
            imagen.Save(path + ImageName);

        }

    }
}