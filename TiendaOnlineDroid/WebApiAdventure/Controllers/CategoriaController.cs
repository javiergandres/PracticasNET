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

namespace WebApiAdventure.Controllers
{
    public class CategoriaController : ApiController
    {
        private AdWorksEntities db = new AdWorksEntities();

        // GET: api/Categoria
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProduct()
        {
            var categorias = from cat in db.ProductCategory
                             select cat;
            List<ProductCategory> catlist = categorias.ToList();

            if (categorias == null)
            {
                return NotFound();
            }

            return Ok(categorias);
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