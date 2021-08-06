using OA.Base.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Dtos.ServiceViewModel
{

    public abstract class SheardUserManageViewModel
    {
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string FullNameAr { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(5)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(9)]
        public string PhoneNumber { get; set; }
        public string UserImage { get; set; }
        public int OrganizationId { get; set; }
    }
    public class UserManageViewModel: SheardUserManageViewModel
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double Blance { get; set; }
        public string OrganizationName { get; set; }
        public bool AccountStatus { get; set; }
        public bool MobileStatus { get; set; }
        public bool Busy { get; set; }
        public bool Archive { get; set; }
        public string DefaultRole { get; set; }
    }
    public class PostUserManageViewModel: SheardUserManageViewModel
    {
        public bool? AccountStatus { get; set; } = true;
        public string Role { get; set; }
    }
    public class PutUserManageViewModel : SheardUserManageViewModel
    {
        public string UserId { get; set; }
        public bool? AccountStatus { get; set; } = false;
        public string Role { get; set; }
    }
    public class ArchiveUserManageViewModel
    {
        public string UserId { get; set; }
        public bool? Archive { get; set; }
    }
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
    }

}
