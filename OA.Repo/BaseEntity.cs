using OA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Data
{
    public abstract class BaseEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public AspNetUser ApplicationUserCreatedBy { get; set; }
        public AspNetUser ApplicationUserUpdatedBy { get; set; }
    }
}
