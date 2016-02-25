using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        [ResponseType(typeof(Product))]
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
                p.StandarCost = producto.StandardCost;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}