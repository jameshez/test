using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace robotjob.Common
{
    public static class TextHandler
    {
        public static string MD5(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";
            source = string.Format("%$#&{0}!&", source);
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5").ToLower();
        }
    }
}
