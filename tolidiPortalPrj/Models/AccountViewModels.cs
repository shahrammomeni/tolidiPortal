using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using pres = Resources.Resource;

namespace tpr.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
    //================================================================================================
    public class ManageUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pasLenth", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pas2")]
        public string ConfirmPassword { get; set; }
    }
    //================================================================================================
    public class LoginViewModel
    {
        //[EmailAddress(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "email2", ErrorMessage = null)]
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class LoginAdminViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [Display(Name = "User name")]
        public string UserName { get; set; }       
    }
    //================================================================================================
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]       
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [StringLength(100,ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pasLenth", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pas2")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        //[EmailAddress(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "email2", ErrorMessage = null)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //==================================
        public int? AccountID { get; set; }          
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        public string Family { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }
    //================================================================================================
    public class RegisterEdit
    {
        //[Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        //[StringLength(100, ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pasLenth", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pas2")]
        //public string ConfirmPassword { get; set; }

        //[Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        // [EmailAddress(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "email2", ErrorMessage = null)]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
        //==================================
        //public int? AccountID { get; set; }
         [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        public string Family { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }
    //================================================================================================
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }
    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pasLenth", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "pas2")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(pres), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
