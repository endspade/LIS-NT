using NGCP.BaseModel;
using System.Security.Cryptography;

namespace NGCP.LIS_NT.Models
{
    public class mIssuance:mGenericRequirement
    {
        public string? docGuid { get; set; }
        public bool docIn { get; set; } = true;

        public string? incomingNo { get; set; }
        public DateTime dateReceived { get; set; }
        public string? asgLawyer { get; set; }
        public DateTime? dateAsgd { get; set; }
        public string? requestor {get;set;}
        public string? docCategory { get; set; }    


        public string? subject { get; set; }
        public string? courtAdmin {get;set;}
        public string? region {get;set;}
        public string? particular {get;set;} //Title


        public DateTime dateReleased { get; set; }
        public string? outgoingNo { get; set; }
        public int nod {get;set;}
        public int revNumber {get;set;}


        public string? docCatNumber { get; set; }
        public string? docCatDesc { get; set; }


        public string? fileName { get; set; }
        public string? fullName { get; set; }
    }
}
