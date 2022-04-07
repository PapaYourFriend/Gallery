using Gallery.ViewModel.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.Models
{
    public class ProfileModel : ValidatorModel
    {
        private int _profileId;
        private string _name;
        private string _surname;
        private string _email;
        private string _dataPath;
        private string _country;
        private string _city;
        private string _address;

        public int ProfileId
        {
            get { return _profileId; }
            set
            {
                _profileId = value;
                RaisePropertyChanged(nameof(ProfileId));
            }
        }
        [Required]
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
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged(nameof(Surname));
            }
        }
        [Required]
        public string DataPath
        {
            get { return _dataPath; }
            set
            {
                _dataPath = value;
                RaisePropertyChanged(nameof(DataPath));
            }
        }
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                RaisePropertyChanged(nameof(Country));
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(nameof(City));
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }
        [Required]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Email не соответствует шаблону")]
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
