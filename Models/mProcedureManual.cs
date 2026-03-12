using NGCP.BaseModel;
using System.Reflection.Emit;

namespace NGCP.LIS_NT.Models
{
    public class mProcedureManual:mGenericRequirement
    {

        public string? docGuid { get; set; }
        public string? docDesc { get; set; }
        public int docRev { get; set; }

        public string? fileName{ get; set; }

    }
}
