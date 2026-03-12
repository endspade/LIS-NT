using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mRPTDocument : mGenericRequirement
    {
        public bool docIn { get; set; }
        public string? docGuid { get; set; }
        public string? pdCode { get; set; }
        public string? documentDesc { get; set; }



        public string? provCode { get; set; }
        public string? munCode { get; set; }
        public string? bgyCode { get; set; }

        public string? provName { get; set; }
        public string? munName { get; set; }
        public string? bgyName { get; set; }


        public string? fileName { get; set; }
        public string? fileType { get; set; }

    }
}
