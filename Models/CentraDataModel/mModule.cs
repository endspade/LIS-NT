using NGCP.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralData.Models
{
    public class mModule : mGenericRequirement
    {
        public string? AppCode { get; set; }
        public string? ModuleCode { get; set; }
        public string? ModuleDesc { get; set; }
        public string? ObjectName { get; set; }
        public bool MCActive { get; set; }
    }
}