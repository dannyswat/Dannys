using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework
{
    public interface IUser<TUserKey>
    {
        TUserKey UserID { get; }
    }
}
