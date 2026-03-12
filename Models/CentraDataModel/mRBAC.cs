using NGCP.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralData.Models
{
    public class mRBAC:mGenericRequirement
    {
        public string? RBACCode { get; set; }
        public string? RBACDesc { get; set; }
        public string? EmpNumber { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime DateFr { get; set; }
        public DateTime DateTo { get; set; }

    }
}