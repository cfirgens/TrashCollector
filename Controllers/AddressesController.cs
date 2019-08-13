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
    public class AddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var loggedIn = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.ApplicationId == loggedIn).FirstOrDefault();
            
            List<Address> addresses = new List<Address>();
            foreach (Address addressList in db.Addresses)
            {
                if (addressList.CustomerId == customer.CustomerId)
                {
                    addresses.Add(addressList);
                }
            }
            return View(addresses);     
        }
        //Get: Addresses/Balance
        public ActionResult Balance ()
        {
            var loggedIn = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.ApplicationId == loggedIn).FirstOrDefault();
                        
            return View(customer);
        }

        //POST: Address/Balance
        [HttpPost, ActionName("Balance")]
        [ValidateAntiForgeryToken]
        public ActionResult Balance( Customer customer)
        {
           
            return View(customer);
        }
    
        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
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

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,StreetAddress,City,CityId,State,ZipCode,pickUpDay,vacationStart,vacationEnd")] Address address)
        {
            if (ModelState.IsValid)
            {
                var loggedIn = User.Identity.GetUserId();
                db.Addresses.Add(address);                
                Customer customer = db.Customers.Where(c => c.ApplicationId == loggedIn).FirstOrDefault();
                address.CustomerId = customer.CustomerId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
        }

        // GET: Addresses/Edit/5
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

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,StreetAddress,City,State,ZipCode,pickUpDay,vacationStart,vacationEnd,SingleDate,CustomerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
