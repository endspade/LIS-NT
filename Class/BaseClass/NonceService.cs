namespace NGCP.BaseClass
{
    public class NonceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NonceService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateNonce()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public void StoreNonceInSession(string nonce)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Session.SetString("Nonce", nonce);
            }
        }

        public string GetNonceFromSession()
        {
            var context = _httpContextAccessor.HttpContext;
            return context?.Session.GetString("Nonce");
        }

        public void UnsetNonceInSession()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Session.Remove("Nonce");
            }
        }




    }
}
