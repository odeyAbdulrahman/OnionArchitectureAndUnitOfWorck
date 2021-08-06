using OA.Base.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace OA.Dtos.ServiceViewModel
{
    public abstract class SheardUserViewModel
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
    public class UserViewModel: SheardUserViewModel
    {
        public double Blance { get; set; }
        public DateTime DateCreated { get; set; }
        public string OrganizationName { get; set; }
    }
    public class PostViewModel: SheardUserViewModel
    {
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string FirebaseToken { get; set; }
    }
    public class PutViewModel: SheardUserViewModel
    {
        public string FirebaseToken { get; set; }
    }
    public class JwtTokenViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public string Role { get; set; }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class OTPViewModel
    {
        [JsonIgnore]
        public int OTPCode { get; set; }
        [JsonIgnore]
        public bool OTPIsUsed { get; set; }
        [JsonIgnore]
        public DateTime OTPDate { get; set; }
        [Required]
        [StringLength(5)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class VerifyOTPViewModel
    {
        [Required]
        public int OTPCode { get; set; }
        [Required]
        [StringLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class FirebaseTokenModel
    {
        public string FirebaseToken { get; set; }
    }
    public class UserHubNotifcationViewModel
    {
        public int Count { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
    }
}
