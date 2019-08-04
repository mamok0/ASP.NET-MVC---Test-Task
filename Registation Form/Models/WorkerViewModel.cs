using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registation_Form.Models
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string HiringDay { get; set; }
        public string Position { get; set; }
        public Nullable<int> CompanyId { get; set; }

        public virtual  CompanyViewModel Company { get; set; }
    }
}