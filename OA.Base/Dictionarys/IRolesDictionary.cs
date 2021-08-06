using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers
{
    public interface IRolesDictionary
    {
        RolesDictionary AddRoles();
        List<KeyValuePair<string, long>> GetRoles();
        string GetRoleKey(long value);
        long GetRoleValue(string key);
    }
}
