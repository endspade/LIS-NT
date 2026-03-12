using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mDocumentCategory:mGenericRequirement
    {
        public string? docType { get; set; }
        public string? docCatNumber { get; set; }
        public string? docCatCode { get; set; }
        public string? docCatDesc { get; set; }

    }
}
