using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstMVC.Models;
namespace CodeFirstMVC.Controllers
{
    
    public class StudentCourseController : Controller
    {
        LTICFEntities db = new LTICFEntities();
        // GET: StudentCourse
        [HttpGet]
        public ActionResult InsertCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertCourse(CourseInfo cinfo)
        {

            if (ModelState.IsValid)
            {
                db.courseInfos.Add(cinfo);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    ModelState.AddModelError("", "Added a new Course");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult InsertStudent()
        {
            ViewData["data"] = new SelectList(db.courseInfos.ToList(), "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public ActionResult InsertStudent(StudentInfo sinfo)
        {

            
           
                ViewData["data"] = new SelectList(db.courseInfos.ToList(), "CourseId", "CourseName");

                
                    db.studentInfos.Add(sinfo);
                    var res = db.SaveChanges();
                    if (res > 0)
                    {
                        ModelState.AddModelError("", "Added a new Student");
                    }
                
            
            return View();
             
        }
    }
}