using Gallery.ViewModel.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.Models
{
    public class ReviewModel : ValidatorModel
    {
        private int _reviewId;
        private int _profileId;
        private string _profileName;
        private string _header;
        private string _message;
        private string _profileDataPath;
        private DateTime _updatedAt;



        public int ReviewId
        {
            get { return _reviewId; }
            set
            {
                _reviewId = value;
                RaisePropertyChanged(nameof(ReviewId));
            }
        }
        [Required]
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }
        [Required]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }
        public DateTime UpdatedAt
        {
            get { return _updatedAt; }
            set
            {
                _updatedAt = value;
                RaisePropertyChanged(nameof(UpdatedAt));
            }
        }
        public int ProfileId
        {
            get { return _profileId; }
            set
            {
                _profileId = value;
                RaisePropertyChanged(nameof(ProfileId));   
            }
        }

        public string ProfileDataPath
        {
            get { return _profileDataPath; }
            set
            {
                _profileDataPath = value;
                RaisePropertyChanged(nameof(ProfileDataPath));
            }
        }
        public string ProfileName
        {
            get { return _profileName; }
            set
            {
                _profileName = value;
                RaisePropertyChanged(nameof(ProfileName));
            }
        }
    }
}
