using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimplePOSWeb.ViewModel.OptionSet;

namespace SimplePOSWeb.ViewModel.Profile
{
    public class UserVM
    {
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nickname is required")]
        public string Nickname { get; set; } = string.Empty;
        #region Change Password
        [DisplayName("Current Password")]
        [Required(ErrorMessage = "Current Password is required")]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string CurrentPassword { get; set; } = null!;
        [DisplayName("New Password")]
        [Required(ErrorMessage = "New Password is required")]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string NewPassword { get; set; } = string.Empty;
        [DisplayName("Re-enter Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string ConfirmPassword { get; set; } = string.Empty;
        #endregion

        public OptionConfigurationVM optionDDL { get; set; }
    }
}
