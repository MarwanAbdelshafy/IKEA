using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;


        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirnPassword { get; set; } = null!;
        [Display(Name = "Is Agree")]

        public bool IsAgree { get; set; }
    }
}
