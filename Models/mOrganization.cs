using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mOrganization:mGenericRequirement
    {
        public string? orgCode { get; set; }
        public string? orgDesc { get; set; }
        public string? empNumber { get; set; }
        public string? fullName { get; set; }

        public DateTime dateFr { get; set; }
        public DateTime dateTo { get; set; }

    }
}
