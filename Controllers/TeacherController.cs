using Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: localhost:xx/Teacher/Index ->
        public ActionResult Index()
        {
            //Navigates to Views/Teacher/Index.cshtml
            return View();
        }
        // GET: Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            List<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher pickedTeacher = controller.FindTeacher(id);

            return View(pickedTeacher);
        }
    }
}