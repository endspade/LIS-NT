using NGCP.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CentralData.Models
{
    public class mUserMaster:mGenericRequirement
    {
        public string? userNumber { get; set; } = "";
        public string? loginName {get;set;} = "";
        public string? passWord { get; set; } = "";
        public DateTime lastLogin {get;set;} = Convert.ToDateTime("01-01-1900");
        public int employeeTypeId { get; set; } = 0;
        public string? employeeNumber { get; set; } = "";
        public string? employeeName { get; set; } = "";
        public DateTime dob {get;set;} = Convert.ToDateTime("01-01-1900");
        public DateTime dateFr {get;set;} = DateTime.Now;
        public DateTime dateTo { get; set; } = DateTime.Now.AddYears(10);
        public DateTime lockoutEnd { get; set; } = Convert.ToDateTime("01-01-1900");
        public int loginAttempt {get;set;} = 0;
        public bool accessActive { get; set; } = true;
        public bool activeDirectory { get; set; } = true;
    }
}