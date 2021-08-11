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
                Roles.Add("User", 1111111111);
                Roles.Add("Editor", 2222222222);
                Roles.Add("CustomerCare", 33333333);
                Roles.Add("Accounts", 444444444);
                Roles.Add("Admin", 555555555);
                Roles.Add("Marketing", 666666666);
                Roles.Add("GeneralDirector", 7777777777);
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
