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
    public class SubCategoriaController : ApiController
    {
        private AdWorksEntities db = new AdWorksEntities();

      

        // GET: api/SubCategoria/5
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult GetProductSubcategory(int idcat)
        {

            var subcategorias = from subcat in db.ProductSubcategory
                                where subcat.ProductCategoryID == idcat
                                select subcat;

            List<SubCategoria> subcatlist = new List<SubCategoria>();
            foreach( var item in subcategorias)
            {
                SubCategoria subcategoria = new SubCategoria();
                subcategoria.CategoryID = item.ProductCategoryID;
                subcategoria.SubCategoryID = item.ProductSubcategoryID;
                subcategoria.SubCategoryName = item.Name;
                subcatlist.Add(subcategoria);

            }
            
            if (subcatlist.Count == 0)
            {
                return NotFound();
            }

            return Ok(subcatlist);
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