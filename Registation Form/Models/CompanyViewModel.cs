using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registation_Form.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> CompanySize { get; set; }
        public string LegalStatus { get; set; }
    }
}