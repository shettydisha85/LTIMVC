using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductMaster.Models;

namespace ProductMaster.Controllers
{
    public class ProductMasterController : Controller
    {

        LTIMVCEntities db = new LTIMVCEntities();
        [HttpGet]
        public ActionResult InsertNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertNewProduct(Product pro)
        {
            pro.PId=Convert.ToInt32(Request.Form["PId"]);
            pro.PName = Request.Form["txtprodname"];
            pro.PDesc = Request.Form["txtdesc"];
            pro.PManu = Request.Form["txtmanu"];
            pro.Price = Convert.ToDecimal(Request.Form["txtprice"]);
            pro.PCat = Request.Form["ddlcat"];
            //this data has to be inserted to DB
            //ado.net or new tech called EF (Entity Framework),it is used for .net app to connect to db
            ModelState.AddModelError("", pro.PName + "," + pro.PDesc + "," + pro.PManu + "," + pro.Price + "," + pro.PCat + ",");
            db.Products.Add(pro);
            int res = db.SaveChanges();
            if (res > 0)
                ModelState.AddModelError("", "new product inserted");
            return RedirectToAction("GetProducts");
        }
        public ActionResult GetProducts() 
        {
            var prod = db.Products.ToList();
            return View(prod);
        }
        [HttpGet]
        public ActionResult GetProductByCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetProductByCategory(string category)
        {
            category = Request.Form["ddlcat"];
            
            var query = from t in db.Products
                        where t.PCat == category
                        select t;
            //var lambda = db.Employees.Where(x => x.Dept == dept && x.Salary >= salary);
            if (query.Count() == 0)
            {
                ModelState.AddModelError("", "No data found for the parameters passed");
                return View();
            }
            else
            {
                //this will pass data from controller to view
                Session["pro"] = query;
            }

            return View();

        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            var data = db.Products.Where(x => x.PId == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateProduct()
        {
            int id = Convert.ToInt32(Request.Form["PId"]);
            var olddata = db.Products.Where(x => x.PId == id).SingleOrDefault();
            var newname = Request.Form["txtprodname"];
            var newdesc = Request.Form["txtdesc"];
            var newmanu = Request.Form["txtmanu"];
            var newprice = Convert.ToDecimal(Request.Form["txtprice"]);
            //var newpid = Convert.ToInt32(Request.Form["txtprodid"]);
            olddata.PName = newname;
            olddata.PDesc = newdesc;
            olddata.PManu = newmanu;
            olddata.Price = newprice;
            //olddata. = newpid;
            olddata.PId = id;
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetProducts");
            return RedirectToAction("InsertNewProduct");
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var data = db.Products.Where(x => x.PId == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteProduct()
        {
            int id = Convert.ToInt32(Request.Form["PId"]);
            var delrow = db.Products.Where(x => x.PId == id).SingleOrDefault();
            db.Products.Remove(delrow);
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetProducts");
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        public ActionResult InsertOrder()
        {
            ViewData["prod"] = new SelectList(db.Products.ToList(),"PId","PId");
            return View();
        }
        [HttpPost]
        public ActionResult InsertOrder(OrderInfo orderinfo)
        {
            orderinfo.pid = Convert.ToInt32(Request.Form["Pid"]);
            orderinfo.orderid = Convert.ToInt32(Request.Form["orderid"]);
            orderinfo.prodqty = Convert.ToInt32(Request.Form["prodqty"]);
            orderinfo.totalamt = Convert.ToInt32(Request.Form["totalamt"]);
            orderinfo.paymentmode = Request.Form["paymentmode"];
            orderinfo.orderstatus= Request.Form["orderstatus"];
            //emp.projid = Convert.ToInt32(Request.Form["ddlpid"]);
            //this data has to be inserted to DB
            //ado.net or new tech called EF (Entity Framework),it is used for .net app to connect to db
            ModelState.AddModelError("", orderinfo.orderid + "," + orderinfo.prodqty + "," + orderinfo.totalamt + "," + orderinfo.paymentmode + "," + orderinfo.orderstatus + "," + orderinfo.pid);
            db.OrderInfoes.Add(orderinfo);
            int res = db.SaveChanges();
            if (res > 0)
                ModelState.AddModelError("", "New Order added");
            return RedirectToAction("GetProducts");
            /*if (ModelState.IsValid)
            {
                db.OrderInfoes.Add(orderinfo);
                var res1 = db.SaveChanges();
                if (res1 > 0)
                    Response.Write("<script type='text/JavaScript'>" + "alert('New Order Created')" + "</script>");
                ModelState.AddModelError("", "New Order Added");

            }*/
            //return View();
        }
        [HttpGet]
        public ActionResult GetOrderDetails()
        {
            var data = (from p in db.Products
                        join o in db.OrderInfoes
                        on p.PId equals o.pid
                        select new CustomOrderDetails { OrderId=o.orderid,ProdName=p.PName,Price=p.Price,Quantity=o.prodqty,TotalAmount=o.totalamt,PaymentMode=o.paymentmode,PaymentStatus=o.orderstatus }).ToList();
            return View(data);
        }
        
        [HttpGet]
        public ActionResult SelectOrderByID()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectOrderByID(int? id, string command)
        {
            if (command == "Display")
            {
                id = Convert.ToInt32(Request.Form["orderid"]);
                var res = db.SelectOrderbyOrderId(id).SingleOrDefault();
                if (res == null)
                {
                    ModelState.AddModelError("", "Invalid Order Id");
                }
                else
                {
                    ModelState.AddModelError("", res.orderid + " ," + res.pid + " , " + res.prodqty + " , " + res.totalamt + " , " + res.paymentmode + " , " + res.orderstatus);
                }
                ViewBag.data = res;
                return View();
            }
            return View();
        }

    }
}