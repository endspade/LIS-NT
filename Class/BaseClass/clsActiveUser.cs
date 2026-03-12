using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace NGCP.BaseClass
{
    public class clsActiveUser
    {


        private IConfiguration? _configuration;

        public clsActiveUser( IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        public static string? _Username(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.Identity?.Name?.Replace("NGCP", "").Replace("//", "").Replace("/", "").Replace("\\", "").ToUpper();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string _PCCode()
        {
            return Environment.MachineName;
        }
        public static PropertyValueCollection GetFromAD(ClaimsPrincipal claimsPrincipal, string propName)
        {
            string userName = claimsPrincipal.Identity?.Name?.Replace("NGCP", "").Replace("//", "").Replace("/", "").Replace("\\", "").ToUpper();
            using (var dsSearcher = new DirectorySearcher())
            {
                var idx = userName.IndexOf('\\');
                if (idx > 0)
                    userName = userName.Substring(idx + 1);
                dsSearcher.Filter = string.Format("(&(objectClass=user)(samaccountname={0}))", userName);
                SearchResult result = dsSearcher.FindOne();
                PropertyValueCollection z = null;
                if (result != null)
                {
                    using (var user = new DirectoryEntry(result.Path))
                    {
                        foreach (PropertyValueCollection p in user.Properties)
                        {
                            if (p.PropertyName == propName)
                            {
                                return p;
                            }
                        }
                    }
                }
                return z;
            }
        }




    }
}