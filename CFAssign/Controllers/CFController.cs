using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFAssign.Models;

namespace CFAssign.Controllers
{
    public class CFController : Controller
    {
        CFAssignEntities db = new CFAssignEntities();
        // GET: CF
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InsertPublisher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertPublisher(Publisher p)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(p);
                var res = db.SaveChanges();
                if (res > 0)
                    ModelState.AddModelError("", "Publisher Added Sucessfully");
                else
                    ModelState.AddModelError("", "Insertion Unsuccessful");
            }
            return View();
        }
        [HttpGet]
        public ActionResult InsertBook()
        {
            ViewData["data"] = new SelectList(db.Publishers.ToList(), "pid", "pname");
            return View();
        }
        [HttpPost]
        public ActionResult InsertBook(Book b)
        {
            ViewData["data"] = new SelectList(db.Publishers.ToList(), "pid", "pname");
            db.Books.Add(b);
            var res = db.SaveChanges();
            if (res > 0)
                ModelState.AddModelError("", "Book Details Added Sucessfully");
            else
                ModelState.AddModelError("", "Insertion Unsuccessful");
            return View();
        }
    }
}