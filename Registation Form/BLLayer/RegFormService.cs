using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Registation_Form.DALayer;
using Registation_Form.Models;
using System.Net;
using System.Web.Mvc;

namespace Registation_Form.BLLayer
{
    public class RegFormService
    {
        WorkersCompaniesDbEntities db = new WorkersCompaniesDbEntities();

        public WorkerDTO workerToDTO (Worker worker)
        {
            return new WorkerDTO
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                MiddleName = worker.MiddleName,
                Position = worker.Position,
                HiringDay = worker.HiringDay,
                CompanyId = worker.CompanyId,
                Company = new CompanyDTO
                {
                    Id = worker.Company.Id,
                    Name = worker.Company.Name,
                    CompanySize = worker.Company.CompanySize,
                    LegalStatus = worker.Company.LegalStatus
                }
            };
        }

        public WorkerDTO workerWmToDTO(WorkerViewModel worker)
        {
            return new WorkerDTO
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                Position = worker.Position,
                MiddleName = worker.MiddleName,
                HiringDay = worker.HiringDay,
                CompanyId = worker.CompanyId,
                Company = new CompanyDTO
                {
                    Id = worker.Company.Id,
                    Name = worker.Company.Name,
                    CompanySize = worker.Company.CompanySize,
                    LegalStatus = worker.Company.LegalStatus
                }
            };
        }

        public CompanyDTO CompanyToDTO (Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                CompanySize = company.CompanySize,
                LegalStatus = company.LegalStatus
            };
        }

        public CompanyDTO CompanyWmToDTO(CompanyViewModel company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                CompanySize = company.CompanySize,
                LegalStatus = company.LegalStatus
            };
        }

        public IEnumerable<WorkerDTO> GetWorkers()
        {
            IList<WorkerDTO> workerDTOs = new List<WorkerDTO>();
            foreach(var item in db.Workers)
            {
                workerDTOs.Add(workerToDTO(item));
            }
            return workerDTOs;
        }

        public IEnumerable<CompanyDTO> GetCompanies()
        {
            IList<CompanyDTO> companyDTOs = new List<CompanyDTO>();
            foreach(var item in db.Companies)
            {
                companyDTOs.Add(CompanyToDTO(item));
            }
            return companyDTOs;
        }

        public void AddCompany( CompanyViewModel companyvm)
        {
            db.Companies.Add(new Company {
                Id = companyvm.Id,
                Name = companyvm.Name,
                CompanySize = companyvm.CompanySize,
                LegalStatus = companyvm.LegalStatus
            });
            db.SaveChanges();
        }

        public void AddWorker(WorkerViewModel workervm)
        {
            db.Workers.Add(new Worker
            {
                Id = workervm.Id,
                Name = workervm.Name,
                Surname = workervm.Surname,
                MiddleName = workervm.MiddleName,
                Position = workervm.Position,
                HiringDay = workervm.HiringDay,
                CompanyId = workervm.CompanyId,
                Company = db.Companies.Find(workervm.CompanyId)
            });
            db.SaveChanges();
        }

        public WorkerDTO GetWorker (int? id)
        {
            Worker worker = db.Workers.Find(id);
            return workerToDTO(worker);
        }

        public CompanyDTO GetCompany(int? id)
        {
            Company company = db.Companies.Find(id);
            return CompanyToDTO(company);
        }

        public void EditCompany(CompanyViewModel companyViewModel)
        {
            Company company = new Company
            {
                Id = companyViewModel.Id,
                Name = companyViewModel.Name,
                CompanySize = companyViewModel.CompanySize,
                LegalStatus = companyViewModel.LegalStatus
            };
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void EditWorker(WorkerViewModel workerViewModel)
        {
            Worker worker = new Worker
            {
                Id = workerViewModel.Id,
                Name = workerViewModel.Name,
                Surname = workerViewModel.Surname,
                MiddleName = workerViewModel.MiddleName,
                Position = workerViewModel.Position,
                HiringDay = workerViewModel.HiringDay,
                CompanyId = workerViewModel.CompanyId,
                Company = db.Companies.Find(workerViewModel.CompanyId)
            };
            db.Entry(worker).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCompany(CompanyViewModel companyViewModel)
        {
            Company company = new Company
            {
                Id = companyViewModel.Id,
                Name = companyViewModel.Name,
                CompanySize = companyViewModel.CompanySize,
                LegalStatus = companyViewModel.LegalStatus
            };
            using (WorkersCompaniesDbEntities db = new WorkersCompaniesDbEntities()) {
                db.Entry(company).State = EntityState.Deleted;
                db.SaveChanges();
            }
            
        }

        public void DeleteWorker(WorkerViewModel workerViewModel)
        {
            Worker worker = new Worker
            {
                Id = workerViewModel.Id,
                Name = workerViewModel.Name,
                Surname = workerViewModel.Surname,
                MiddleName = workerViewModel.MiddleName,
                Position = workerViewModel.Position,
                HiringDay = workerViewModel.HiringDay,
                CompanyId = workerViewModel.CompanyId,
                Company = new Company
                {
                    Id = workerViewModel.Company.Id,
                    Name = workerViewModel.Company.Name,
                    CompanySize = workerViewModel.Company.CompanySize,
                    LegalStatus = workerViewModel.Company.LegalStatus
                }
            };
            using (WorkersCompaniesDbEntities db = new WorkersCompaniesDbEntities())
            {
                db.Entry(worker).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}