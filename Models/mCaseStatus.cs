using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mCaseStatus:mGenericRequirement
    {
        public string? caseStatusNumber { get; set; }
        public string? caseStatusDesc { get; set; }
    }
}
