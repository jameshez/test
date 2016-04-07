using robotjob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace robotjob.IDAL
{
    public interface IUser
    {
        Sys_Customer GetUser(string userName);
    }
}
