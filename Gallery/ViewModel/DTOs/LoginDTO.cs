using Gallery.ViewModel.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.DTOs
{
    public class LoginDTO : ValidatorModel
    {
        private string _email;
        private string _password;

        public LoginDTO()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }
        [Required]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
    }
}
