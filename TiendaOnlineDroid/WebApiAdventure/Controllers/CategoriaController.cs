﻿using System;
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
    public class CategoriaController : ApiController
    {
        private AdWorksEntities db = new AdWorksEntities();

        // GET: api/Categoria
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProduct()
        {
            var categorias = from cat in db.ProductCategory
                             select cat;
          
            List<Categoria> catlist = new List<Categoria>();
            foreach(var item in categorias)
            {
                Categoria categoria = new Categoria();
                categoria.CategoryID = item.ProductCategoryID;
                categoria.CategoryName = item.Name;
                catlist.Add(categoria);
            }


            if (catlist == null)
            {
                return NotFound();
            }

            return Ok(catlist);
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