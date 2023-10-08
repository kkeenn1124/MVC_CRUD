using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_PT.Controllers
{
    public class SaleController : Controller
    {
        CompanyDataEntities db=new CompanyDataEntities();
        // GET: Sale
        public ActionResult Index()
        {
            var table = db.employee;
            List<employee> emp=table.ToList();
            ViewBag.list=emp;
            ViewBag.count=emp.Count();
            return View();
        }

        public ActionResult Update(string employeeID)
        {
            var table = db.employee.ToList().Single(_ => _.employeeID == employeeID);
            ViewBag.list = table;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateInfo(string employeeID)
        {
            var table = db.employee.ToList().Single(_=>_.employeeID==employeeID);
            TryUpdateModel(table);
            db.SaveChanges();
            return Redirect("Index");
        }

        public ActionResult Delete(string employeeID)
        {
            var table = db.employee.ToList().Where(_ => _.employeeID == employeeID);
            db.employee.RemoveRange(table);
            db.SaveChanges();
            return Redirect("Index");
        }

        public ActionResult Insertemp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insertemp(employee employee)
        {
            if (db.employee.Where(_=>_.employeeID==employee.employeeID).FirstOrDefault()==null)
            {
                db.employee.Add(employee);
                db.SaveChanges();
            }
            else
            {
                return Content("此工號已註冊");
            }
            return Redirect("Index");
        }

        public ActionResult Select(employee employee)
        {
            int tablecount = 0;
            if (employee.employeeID!=null)
            {
                var table = db.employee.Where(_ => _.employeeID == employee.employeeID).ToList();
                tablecount = table.Count();
                ViewBag.list = table;
                ViewBag.count = table.Count;
            }
            else if(employee.employeeName!=null)
            {
                var table = db.employee.Where(_ => _.employeeName == employee.employeeName).ToList();
                tablecount = table.Count();
                ViewBag.list = table;
                ViewBag.count = table.Count;
            }
            else
            {
                var table = db.employee.Where(_ => _.department == employee.department).ToList();
                tablecount = table.Count();
                ViewBag.list = table;
                ViewBag.count = table.Count;
            }
            
            //var table=db.employee.Where(_=>_.employeeName == employee.employeeName).ToList();
            return View();
        }
    }
}