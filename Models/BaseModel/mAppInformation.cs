namespace NGCP.BaseModel

{
    public class mAppInformation
    {
        public static string applicationCode { get; set; } = "036000";
        public static string applicationName { get; set; } = "Legal Information System";
        public static string applicationShortName { get; set; } = "LIS";
        public static Guid docGuid { get; set; } = Guid.NewGuid();


    }
}
