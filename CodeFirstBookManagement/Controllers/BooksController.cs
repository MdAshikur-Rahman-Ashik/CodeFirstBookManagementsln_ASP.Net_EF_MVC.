using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstBookManagement.DAL;
using CodeFirstBookManagement.Models;

namespace CodeFirstBookManagement.Controllers
{
    public class BooksController : Controller
    {
        private BookDBEntities db = new BookDBEntities(); 
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        } 
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookName")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

    }
}
