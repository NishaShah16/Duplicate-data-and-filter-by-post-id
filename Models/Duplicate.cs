using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplicateReport.Models
{
    public class Duplicate
    {
        public int id { get; set; }
        public int IndexNo { get; set; }

        public string RegistrationNo { get; set; }
        public string CandidateName { get; set; }
        public string PhotoPath { get; set; }
        public string post { get; set; }
        public int post_Id { get; set; }

    }
}