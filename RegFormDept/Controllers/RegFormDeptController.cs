using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegFormDept.Controllers
{
    public class RegFormDeptController : Controller
    {
        [HttpGet]
        public ActionResult InsertDepartment()
        {
            return View();
        }
        [HttpPost]

        public ActionResult InsertDepartment(Department dept)
        {

        }
    }
}