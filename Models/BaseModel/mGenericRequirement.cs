namespace NGCP.BaseModel
{
    public class mGenericRequirement
    {
        public string _action { get; set; } = "";
        public string id { get; set; } = "0";
        public string remarks { get; set; } = "";
        public bool recordStatus { get; set; } = true;
        public string userCode { get; set; } = "";
        public string pcCode { get; set; } = Environment.MachineName;
        public DateTime dateStamp { get; set; } = DateTime.Now;
        public string guid { get; set; } = "";
        public string appCode { get; set; } = mAppInformation.applicationCode;
    }
}
