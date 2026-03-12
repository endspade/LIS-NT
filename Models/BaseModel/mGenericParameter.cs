

namespace NGCP.BaseModel
{
    public class mGenericParameter
    {
        public string? _action { get; set; } = "A";
        public string? strParam { get; set; } = "";
        public int intParam { get; set; } = 0;
        public DateTime dateFr { get; set; } = DateTime.Now;
        public DateTime dateTo { get; set; } = DateTime.Now.AddYears(1);


        public string? appCode { get; set; } = mAppInformation.applicationCode;
        public string? rbacCode { get; set; } = "";
        public string? moduleCode { get; set; } =  "";

        public string? provCode { get; set; } = "";
        public string? munCode { get; set; } = "";
        public string? bgyCode { get; set; } = "";
    }
}
