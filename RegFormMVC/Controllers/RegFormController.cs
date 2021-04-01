using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegFormMVC.Models;


namespace RegFormMVC.Controllers
{
    public class RegFormController : Controller
    {
        LTIMVCEntities db = new LTIMVCEntities();
        [HttpGet]
        public ActionResult InsertNewEmployee()
        {
            ViewData["proj"] = new SelectList(db.ProjectInfoes.ToList(), "projid", "projname");
            return View();
        }
        [HttpPost]
        public ActionResult InsertNewEmployee(Employee emp)
        {
            emp.EmpID = Convert.ToInt32(Request.Form["txtempid"]);
            emp.EmpName = Request.Form["txtempname"];
            emp.Dept = Request.Form["ddldept"];
            emp.Desg = Request.Form["ddldesg"];
            emp.Salary = Convert.ToDecimal(Request.Form["txtsalary"]);
            emp.projid = Convert.ToInt32(Request.Form["ddlpid"]);
            //this data has to be inserted to DB
            //ado.net or new tech called EF (Entity Framework),it is used for .net app to connect to db
            ModelState.AddModelError("", emp.EmpID + "," + emp.EmpName + "," + emp.Dept + "," + emp.Desg + "," + emp.Salary+","+emp.projid);
            db.Employees.Add(emp);
            int res = db.SaveChanges();
            if (res > 0)
                ModelState.AddModelError("", "new employee inserted");
            return RedirectToAction("GetEmployees");
        }
        //Retreive all emp details from db when the form is loading
        public ActionResult GetEmployees()
        {
            var data = db.Employees.ToList();
            return View(data); //model binding
        }
        [HttpGet]
        public ActionResult GetEmployeeByDeptSalary()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEmployeeByDeptSalary(string dept,decimal? salary)
        {
            dept = Request.Form["txtdept"];
            salary = Convert.ToDecimal(Request.Form["txtsalary"]);
            var query = from t in db.Employees
                        where t.Dept == dept && t.Salary >= salary
                        select t;
            var lambda = db.Employees.Where(x => x.Dept == dept && x.Salary >= salary);
            if(query.Count()==0)
            {
                ModelState.AddModelError("", "No data found for the parameters passed");
                return View();
            }
            else
            {
                //this will pass data from controller to view
                Session["data"] = query;
            }

            return View();

        }
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var data = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateEmployee()
        {
            int id = Convert.ToInt32(Request.Form["eid"]);
            var olddata = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
            var newname = Request.Form["name"];
            var newdept = Request.Form["dept"];
            var newdesg = Request.Form["desg"];
            var newsal = Convert.ToDecimal(Request.Form["sal"]);
            var newpid = Convert.ToInt32(Request.Form["pid"]);
            olddata.EmpName = newname;
            olddata.Dept = newdept;
            olddata.Desg = newdesg;
            olddata.Salary = newsal;
            olddata.projid = newpid;
            olddata.EmpID = id;
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetEmployees");
           return RedirectToAction("InsertNewEmployee");
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            var data = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteEmployee()
        {
            int id = Convert.ToInt32(Request.Form["eid"]);
            var delrow = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
            db.Employees.Remove(delrow);
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetEmployees");
            return RedirectToAction("GetEmployees");
        }
        [HttpGet]
        public ActionResult InsertProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertProject(ProjectInfo pinfo)
        {
           if(ModelState.IsValid)
            {
                db.ProjectInfoes.Add(pinfo);
                var res = db.SaveChanges();
                if (res > 0)
                    Response.Write("<script type='text/JavaScript'>" + "alert('New Project Created')" + "</script>");
                ModelState.AddModelError("", "New Project Added");

            }
            return View();
        }
        [HttpGet]
        public ActionResult GetEmpProject()
        {
            var data = (from e in db.Employees
                        join p in db.ProjectInfoes
                        on e.projid equals p.projid
                        select new CustomEmpProject { EmpId = e.EmpID, Name = e.EmpName, Dept = e.Dept, ProjName = p.projname, Domain = p.domain }).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult SelectandUpdateProjectSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SelectAndUpdateProjectSP(string command)
        {
            if (command == "Select" || command=="select")
            {
                int? id = Convert.ToInt32(Request.Form["pid"]);
                var result = db.sp_SelectProjectbyId(id).SingleOrDefault();
                if (result == null)
                    ModelState.AddModelError("", "Invalid Id");
                else
                    ModelState.AddModelError("", result.projid + "," + result.projname + "," + result.domain);

                ViewBag.data = result; //To display data from controller to view

                return View();
            }
            else if (command == "Update" || command=="update")
            {
                int projid = Convert.ToInt32(Request.Form["projid"]);
                string newname = Request.Form["pname"];
                string newdomain = Request.Form["pdomain"];
                var res = db.sp_updateProject(projid, newname, newdomain);
                if (res >= 0)
                {
                    ModelState.AddModelError("", "Data Updated");
                }
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}