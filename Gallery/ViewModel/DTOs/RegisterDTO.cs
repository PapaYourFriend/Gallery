using Gallery.ViewModel.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.DTOs
{
    public class RegisterDTO : ValidatorModel
    {
        private string _name;
        private string _password;
        private string _email;

        public RegisterDTO()
        {

        }

        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Имя неверно введено!")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^[\w~'`!@#№?$%^&*()=+<>|/\\.,:;\[\]{} \x22-]{6,32}$", ErrorMessage = "Пароль должен быть без русских символов")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не меньше 6 символов")]
        [MaxLength(32, ErrorMessage = "Пароль должен быть не больше 32 символов")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([a-zA-Z0-9_-]+\.)*[a-zA-Z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Email не соответствует шаблону")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }
    }
}
