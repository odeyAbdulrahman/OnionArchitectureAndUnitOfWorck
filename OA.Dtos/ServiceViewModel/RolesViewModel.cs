using OA.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Dtos.ServiceViewModel
{
    public class UserMangeRolesViewModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
