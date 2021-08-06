using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace OA.Base.Helpers
{
    public class RolesDictionary : IRolesDictionary
    {
        public Dictionary<string, long> Roles = new Dictionary<string, long>(7);
        public RolesDictionary AddRoles()
        {
            if (Roles.Count() < 7)
            {
                Roles.Add("User", 231542980);
                Roles.Add("Editor", 411880522);
                Roles.Add("CustomerCare", 533384120);
                Roles.Add("Accounts", 60004320);
                Roles.Add("Admin", 110579020);
                Roles.Add("Marketing", 531300120);
                Roles.Add("GeneralDirector", 120306380);
            }
            return this;
        }

        public List<KeyValuePair<string, long>> GetRoles()
        {
            return Roles.ToList();
        }
        public string GetRoleKey(long value)
        {
            return Roles.FirstOrDefault(x => x.Value == value).Key;
        }
        public long GetRoleValue(string key)
        {
            return Roles.FirstOrDefault(x => x.Key == key).Value;
        }
    }
}
