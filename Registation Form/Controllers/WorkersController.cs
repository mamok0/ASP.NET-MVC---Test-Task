using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Registation_Form.Models;

namespace Registation_Form.Controllers
{
    public class WorkersController : Controller
    {
        private WorkersCompaniesDbEntities db = new WorkersCompaniesDbEntities();

        // GET: Workers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WorkersList()
        {
            var workers = db.Workers.Include(w => w.Company);
            return View(workers.ToList());
        }

        public ActionResult CompaniesList()
        {
            var companies = db.Companies.ToList();
            return View(companies);
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompany([Bind(Include = "Id,Name,CompanySize,LegalStatus")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("CompaniesList");
            }
            return View();
        }

        // GET: Workers/Create
        public ActionResult AddWorker()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorker([Bind(Include = "Id,Surname,Name,MiddleName,HiringDay,Position,CompanyId")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Workers.Add(worker);
                db.SaveChanges();
                return RedirectToAction("WorkersList");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", worker.CompanyId);
            return View(worker);
        }

        // GET: Workers/Edit/5
        public ActionResult EditWorker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", worker.CompanyId);
            return View(worker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWorker([Bind(Include = "Id,Surname,Name,MiddleName,HiringDay,Position,CompanyId")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WorkersList");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", worker.CompanyId);
            return View(worker);
        }

        public ActionResult EditCompany(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany([Bind(Include = "Id,Name,CompanySize,LegalStatus")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CompaniesList");
            }
            //ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", company.CompanyId);
            return View(company);
        }

        // GET: Workers/Delete/5
        public ActionResult DeleteWorker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        [HttpPost, ActionName("DeleteWorker")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWorkerConfirmed(int id)
        {
            Worker worker = db.Workers.Find(id);
            db.Workers.Remove(worker);
            db.SaveChanges();
            return RedirectToAction("WorkersList");
        }

        public ActionResult DeleteCompany(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("DeleteCompany")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCompanyConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("CompaniesList");
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
