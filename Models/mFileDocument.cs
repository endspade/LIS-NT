using NGCP.BaseModel;

namespace NGCP.LIS_NT.Models
{
    public class mFileDocument:mGenericRequirement
    {
        public string? docGuid { get; set; }
        public string? refType { get; set; } = "";
        public string? refNumber { get; set; } = "";
        public string? fileName { get; set; } = "";
        public byte[] file { get; set; } = new byte[0];
        public string? fileType { get; set; } = "";

        public string? fileSize { get; set; } = "";
    }
}
