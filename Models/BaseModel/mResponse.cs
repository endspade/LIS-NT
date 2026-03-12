namespace NGCP.BaseModel
{
    public class mResponse
    {
        public int ResponseCode { get; set; } = 404;
        public string ResponseMessage { get; set; } = "Not Found!";
        public string? ResponseData { get; set; } = null;
    }
}
