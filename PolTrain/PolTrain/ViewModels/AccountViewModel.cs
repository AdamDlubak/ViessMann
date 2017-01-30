using System.ComponentModel.DataAnnotations;

namespace PolTrain.ViewModels
{
    public class AccountViewModel
    {
//    public class LoginViewModel
//    {
//        [Required(ErrorMessage = "Musisz wprowadzić e-mail")]
//        [Display(Name = "Email")]
//        [EmailAddress]
//        public string Email { get; set; }
//
//        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
//        [DataType(DataType.Password)]
//        [Display(Name = "Hasło")]
//        public string Password { get; set; }
//
//        [Display(Name = "Zapamiętaj mnie")]
//        public bool RememberMe { get; set; }
//    }

        public class RegisterViewModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            [RegularExpression(@"(\+\d{2})*[\d\s-]+",
                 ErrorMessage = "Błędny format numeru telefonu.")]
            public string PhoneNumber { get; set; }


        }

        public class ExternalLoginConfirmationViewModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

    }
}