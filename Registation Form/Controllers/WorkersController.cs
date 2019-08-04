using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Registation_Form.BLLayer;
using Registation_Form.Models;

namespace Registation_Form.Controllers
{
    public class WorkersController : Controller
    {
        RegFormService regFormService = new RegFormService();

        public WorkerViewModel workerDTOtoVM(WorkerDTO workerDTO)
        {
            return new WorkerViewModel
            {
                Id = workerDTO.Id,
                Name = workerDTO.Name,
                Surname = workerDTO.Surname,
                MiddleName = workerDTO.MiddleName,
                Position = workerDTO.Position,
                HiringDay = workerDTO.HiringDay,
                CompanyId = workerDTO.CompanyId,
                Company = new CompanyViewModel
                {
                    Id = workerDTO.Company.Id,
                    Name = workerDTO.Company.Name,
                    CompanySize = workerDTO.Company.CompanySize,
                    LegalStatus = workerDTO.Company.LegalStatus
                }
            };
        }

        public CompanyViewModel companyDTOtoVM(CompanyDTO companyDTO)
        {
            return new CompanyViewModel
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                CompanySize = companyDTO.CompanySize,
                LegalStatus = companyDTO.LegalStatus
            };
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WorkersList()
        {
            var workers = regFormService.GetWorkers().Select(x => workerDTOtoVM(x));
            return View(workers.ToList());
        }

        public ActionResult CompaniesList()
        {
            var companies = regFormService.GetCompanies().Select(x => companyDTOtoVM(x));
            return View(companies);
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompany([Bind(Include = "Id,Name,CompanySize,LegalStatus")] CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                regFormService.AddCompany(company);
                return RedirectToAction("CompaniesList");
            }
            return View();
        }

        // GET: Workers/Create
        public ActionResult AddWorker()
        {
            ViewBag.CompanyId = new SelectList(regFormService.GetCompanies(), "Id", "Name");
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorker([Bind(Include = "Id,Surname,Name,MiddleName,HiringDay,Position,CompanyId")] WorkerViewModel worker)
        {
            if (ModelState.IsValid)
            {
                regFormService.AddWorker(worker);
                return RedirectToAction("WorkersList");
            }

            return View(worker);
        }

        // GET: Workers/Edit/5
        public ActionResult EditWorker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkerViewModel worker = workerDTOtoVM(regFormService.GetWorker(id));
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(regFormService.GetCompanies(), "Id", "Name", worker.CompanyId);
            return View(worker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWorker([Bind(Include = "Id,Surname,Name,MiddleName,HiringDay,Position,CompanyId")] WorkerViewModel worker)
        {
            if (ModelState.IsValid)
            {
                regFormService.EditWorker(worker);
                return RedirectToAction("WorkersList");
            }
            ViewBag.CompanyId = new SelectList(regFormService.GetCompanies(), "Id", "Name", worker.CompanyId);
            return View(worker);
        }

        public ActionResult EditCompany(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel company = companyDTOtoVM(regFormService.GetCompany(id));
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany([Bind(Include = "Id,Name,CompanySize,LegalStatus")] CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                regFormService.EditCompany(company);
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
            WorkerViewModel worker = workerDTOtoVM(regFormService.GetWorker(id));
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
            WorkerViewModel worker = workerDTOtoVM(regFormService.GetWorker(id));
            regFormService.DeleteWorker(worker);
            return RedirectToAction("WorkersList");
        }

        public ActionResult DeleteCompany(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel company = companyDTOtoVM(regFormService.GetCompany(id));
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
            CompanyViewModel company = companyDTOtoVM(regFormService.GetCompany(id));
            regFormService.DeleteCompany(company);
            return RedirectToAction("CompaniesList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                regFormService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
