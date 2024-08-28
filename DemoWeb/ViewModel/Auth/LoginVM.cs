using System.ComponentModel.DataAnnotations;

namespace SimplePOSWeb.ViewModel.Auth
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter username!")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Please enter password!")]
        public required string Password { get; set; }
    }
}
