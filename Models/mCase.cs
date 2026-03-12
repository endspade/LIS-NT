using Microsoft.Extensions.Primitives;
using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mCase:mGenericRequirement
    {
        //case header

        public string? orgType { get; set; }
        public string? docNumber { get; set; }
        public string? caseType { get; set; }
        public string? caseNumber { get; set; }
        public string? caseTitle { get; set; }
        public string? caseDesc { get; set; }
        public string? caseNature { get; set; }
        public string? caseStatus { get; set; }
        public string? caseLevel { get; set; }
        public string? venue { get; set; }
        public decimal amount { get; set; }



        //case details
        public string? docGuid { get; set; } 
        public string? refNumber { get; set; }
        public string? incomingNo { get; set; }
        public string? outgoingNo { get; set; }
        public string? petitioner { get; set; }
        public string? judge { get; set; }
        public string? docCategory { get; set; }
        public string? documentDesc { get; set; }
        public DateTime dateReceived { get; set; }  
        public DateTime dateNxtHiring { get; set; }
        public string? pendingIncident { get; set; }
        public bool docIn { get; set; }


        public string? caseTypeDesc { get; set; }
        public string? caseLevelDesc { get; set; }
        public string? caseStatusDesc { get; set; }
        public string? docCatCode { get; set; }
        public string? docCatDesc { get; set; }
        


        public string? fileId { get; set; }
        public string? fileName { get; set; }
        public string? fileType { get; set; }
    }
}
