using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace robotjob.Common
{
    public class TimeHelper
    {
        public static string GetTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 + "";
        }
    }
}
