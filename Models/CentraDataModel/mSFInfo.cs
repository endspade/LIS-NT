
using NGCP.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace CentralData.Models
{
    public class mSFInfo:mGenericRequirement
    {
        public string? appDesc { get; set; } = mAppInformation.applicationCode;
        public string? moduleCode { get; set; }
        public string? moduleDesc { get; set; }
        public string? rbacCode { get; set; }
        public string? sfInfo { get; set; }

    }
}