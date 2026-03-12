using NGCP.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralData.Models
{
    public class mLogin
    {
        public string? userName { get; set; }
        public string? userPass { get; set; }
        public string? appCode { get; set; } = mAppInformation.applicationCode;
    }
}