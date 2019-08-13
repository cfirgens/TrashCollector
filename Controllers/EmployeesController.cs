using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var loggedIn = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(c => c.ApplicationId == loggedIn).FirstOrDefault();
                

            string todaysDay = DateTime.Now.DayOfWeek.ToString();
            
            List<Address> customersToday = new List<Address>();
            foreach (Address address in db.Addresses)
            {                
                if (address.ZipCode == employee.zipCode && address.pickUpDay == todaysDay)
                {
                    customersToday.Add(address);
                }
            }

            return View(customersToday);
        }

        //GET: Employees/ALLPU
        // GET: Employees
        public ActionResult AllPU()
        {
            var loggedIn = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(c => c.ApplicationId == loggedIn).FirstOrDefault();


            string todaysDay = DateTime.Now.DayOfWeek.ToString();

            List<Address> customersToday = new List<Address>();
            foreach (Address address in db.Addresses)
            {
                if (address.ZipCode == employee.zipCode)
                {
                    customersToday.Add(address);
                }
            }

            return View(customersToday);
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeNumber,firstName,lastName,zipCode")] Employee employee)
        {
            var userId = User.Identity.GetUserId();
            employee.ApplicationId = userId;

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit(int id, Address address)
        {
            var addressCharged = db.Addresses.Where(a => a.AddressId == id).FirstOrDefault();
            var customerCharged = db.Customers.Where(c => c.CustomerId == addressCharged.CustomerId).FirstOrDefault();
            customerCharged.Balance += 20;     
            addressCharged.PickedUp = true;
            db.SaveChanges();

            return RedirectToAction("Index", "Employees");
        }
        
        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
