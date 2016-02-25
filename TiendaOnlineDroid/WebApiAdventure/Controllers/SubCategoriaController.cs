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
    public class SubCategoriaController : ApiController
    {
        private AdWorksEntities db = new AdWorksEntities();

        // GET: api/SubCategoria
        public IQueryable<ProductSubcategory> GetProductSubcategory()
        {
            return db.ProductSubcategory;
        }

        // GET: api/SubCategoria/5
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult GetProductSubcategory(int id)
        {
            ProductSubcategory productSubcategory = db.ProductSubcategory.Find(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            return Ok(productSubcategory);
        }

        // PUT: api/SubCategoria/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSubcategory(int id, ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productSubcategory.ProductSubcategoryID)
            {
                return BadRequest();
            }

            db.Entry(productSubcategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSubcategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SubCategoria
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult PostProductSubcategory(ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductSubcategory.Add(productSubcategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productSubcategory.ProductSubcategoryID }, productSubcategory);
        }

        // DELETE: api/SubCategoria/5
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult DeleteProductSubcategory(int id)
        {
            ProductSubcategory productSubcategory = db.ProductSubcategory.Find(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            db.ProductSubcategory.Remove(productSubcategory);
            db.SaveChanges();

            return Ok(productSubcategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductSubcategoryExists(int id)
        {
            return db.ProductSubcategory.Count(e => e.ProductSubcategoryID == id) > 0;
        }
    }
}