using CodeFirstBookManagement.DAL;
using CodeFirstBookManagement.Models.ViewModel;
using CodeFirstBookManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CodeFirstBookManagement.Controllers
{
    public class CustomersController : Controller
    {
        private BookDBEntities _db = new BookDBEntities();
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.dateSortparam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var customers = _db.Customers.OrderByDescending(e => e.CustomerId).Include(d => d.BookEntries.Select(c => c.Book));
            
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.CustomerName.ToUpper().
                Contains(searchString.ToUpper()));


            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.CustomerName);
                    break;
                case "Date":
                    customers = customers.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(s => s.BirthDate);
                    break;
                default:
                    customers = customers.OrderBy(s => s.CustomerName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddNewBook(int? id)
        {
            ViewBag.Books = new SelectList(_db.Books.ToList(), "BookId", "BookName", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewBook");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel vObj, int[] bookId)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerName = vObj.CustomerName,
                    BirthDate = vObj.BirthDate,
                    IsRegular = vObj.IsRegular,
                    Diposite=vObj.Diposite,
                    Email=vObj.Email,

                };
                HttpPostedFileBase file = vObj.PicturePath;
                string filepath = Path.Combine("/images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                file.SaveAs(Server.MapPath(filepath));
                customer.Picture = filepath;

                foreach (var item in bookId)
                {
                    BookEntry bo = new BookEntry()
                    {
                        Customer = customer,
                        CustomerId = customer.CustomerId,
                        BookId = item
                    };
                    _db.BookEntries.Add(bo);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Customer customer = _db.Customers.First(x => x.CustomerId == id);
            var books = _db.BookEntries.Where(x => x.CustomerId == id).ToList();
            CustomerViewModel vObj = new CustomerViewModel()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                BirthDate = customer.BirthDate,
                IsRegular = customer.IsRegular,
                Picture = customer.Picture,
                Email = customer.Email,
                Diposite = customer.Diposite,
            };
            if (books.Any())
            {
                foreach (var item in books)
                {
                    vObj.BookList.Add(item.BookId);
                }

            }
            return View(vObj);
        }
        [HttpPost]
        public ActionResult Edit(CustomerViewModel vObj, int[] bookId)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerName = vObj.CustomerName,
                    BirthDate = vObj.BirthDate,
                    IsRegular = vObj.IsRegular,
                    Diposite= vObj.Diposite,
                    Email = vObj.Email,
                    CustomerId = vObj.CustomerId

                };
                HttpPostedFileBase file = vObj.PicturePath;
                if (file != null)
                {
                    string filepath = Path.Combine("/images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filepath));
                    customer.Picture = filepath;
                }
                else
                {
                    customer.Picture = vObj.Picture;
                }
                var existingBook = _db.BookEntries.Where(x => x.CustomerId == customer.CustomerId).ToList();
                foreach (var item in existingBook)
                {
                    _db.BookEntries.Remove(item);
                }
                foreach (var item in bookId)
                {
                    BookEntry bo = new BookEntry()
                    {

                        CustomerId = customer.CustomerId,
                        BookId = item
                    };

                    _db.BookEntries.Add(bo);
                }
                _db.Entry(customer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Delete(int? id)
        {
            var bmp = _db.Customers.Find(id);
            var exisBook = _db.BookEntries.Where(e => e.CustomerId == id).ToList();
            foreach (var item in exisBook)
            {
                _db.BookEntries.Remove(item);
            }
            _db.Entry(bmp).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
