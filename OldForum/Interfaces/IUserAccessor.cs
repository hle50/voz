using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldForum.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUserName();
    }
}
