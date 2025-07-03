using System;
using System.Web;

namespace WEBDOGO
{
    public static class AppConfig
    {
        // Tên server
        public static string ServerName
        {
            get
            {
                var request = HttpContext.Current.Request;
                return $"{request.Url.Scheme}://{request.Url.Authority}/";
            }
        }
    }
}
