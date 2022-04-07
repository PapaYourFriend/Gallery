using Gallery.Core.DomainModels;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.DTOs
{
    public class OrderDTO : ValidatorModel
    {
        private string _country;
        private string _city;
        private string _address;

        public OrderDTO(PictureModel picture, Profile profile)
        {
            Country = profile.Country;
            City = profile.City;
            Address = profile.Address;
            Picture = picture;
        }
        public PictureModel Picture { get; set; } 
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                RaisePropertyChanged(nameof(Country));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(nameof(City));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }
    }
}
