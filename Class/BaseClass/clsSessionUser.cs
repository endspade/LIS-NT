using CentralData.Class;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using NGCP.BaseModel;
namespace NGCP.BaseClass
{
    public class clsSessionUser
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public clsSessionUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool session_active()
        {
            try
            { 
                HttpContext? context = _httpContextAccessor.HttpContext;
                string? user = context?.Session.GetString("_userName");
                if (context != null || user?.Length == 0)
                {
           
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void session_set_userName( string param)
        {

            var context = _httpContextAccessor.HttpContext;
            context?.Session.SetString("_userName", param);


        }
        public string? session_get_username()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                return context?.Session.GetString("_userName");
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public void session_set_userNumber(string param)
        {  
            var context = _httpContextAccessor.HttpContext;
            context?.Session.SetString("_userNumber",param);
           
        }
        public string? session_get_userNumber()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                return context?.Session.GetString("_userNumber");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void session_set(string key,string value)
        {

            var context = _httpContextAccessor.HttpContext;
            context?.Session.SetString(key, value);


        }
        public string? session_get(string key)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                return context?.Session.GetString(key);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void session_unset()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Session.Remove("_userName");
                context.Session.Remove("_userNumber");
                context.Session.Remove("_userDiv");

            }
        }


       

    }
}
