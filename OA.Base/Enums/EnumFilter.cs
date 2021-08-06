using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers
{
    public enum EnumUserSearchBy : int
    {
        ByName = 1,
        ByEmail = 2,
        ByPhone = 3,
        Archive = 4
    }

    public enum EnumSettingSearchBy : int
    {
        Type = 1,
        Name = 2,
    }
}
