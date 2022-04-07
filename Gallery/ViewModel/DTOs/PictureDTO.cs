using Gallery.ViewModel.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gallery.ViewModel.DTOs
{
    public class PictureDTO : ValidatorModel
    {
        private string _pictureName;
        private string _location;
        private string _description;
        private string _dataPath;
        private string _artistName;
        private string _artistSurname;
        private int _artistId = 1;
        private int _styleId = 1;
        private string _styleName;
        private DateTime _createdAt = DateTime.Now;
        private string _price;
        private bool _existArtist = false;
        private bool _existStyle = false;


        public PictureDTO()
        {
            PictureName = string.Empty; 
        }
        public bool ExistArtist
        {
            get { return _existArtist; }
            set
            {
                _existArtist = value;
                if(_existArtist)
                {
                    ArtistName = "null";
                    ArtistSurname = "null";
                }
                else
                {
                    ArtistName = string.Empty;
                    ArtistSurname = string.Empty;
                }
                RaisePropertyChanged(nameof(ExistArtist));
            }
        }
        public bool ExistStyle
        {
            get { return _existStyle; }
            set
            {
                _existStyle = value;
                if(_existStyle)
                {
                    StyleName = "null";
                }
                else
                {
                    StyleName = string.Empty;
                }
                RaisePropertyChanged(nameof(ExistStyle));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string PictureName
        {
            get { return _pictureName; }
            set
            {
                _pictureName = value;
                RaisePropertyChanged(nameof(PictureName));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                decimal p;
                if (!decimal.TryParse(value, out p))
                {
                    _price = string.Empty;
                }
                RaisePropertyChanged(nameof(Price));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                _createdAt = value;
                RaisePropertyChanged(nameof(CreatedAt));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string DataPath
        {
            get { return _dataPath; }
            set
            {
                _dataPath = value;
                RaisePropertyChanged(nameof(DataPath));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                _artistName = value;
                RaisePropertyChanged(nameof(ArtistName));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string ArtistSurname
        {
            get { return _artistSurname; }
            set
            {
                _artistSurname = value;
                RaisePropertyChanged(nameof(ArtistSurname));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string StyleName
        {
            get { return _styleName; }
            set
            {
                _styleName = value;
                RaisePropertyChanged(nameof(StyleName));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public int StyleId
        {
            get { return _styleId; }
            set
            {
                _styleId = value;
                RaisePropertyChanged(nameof(StyleId));
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        public int ArtistId
        {
            get { return _artistId; }
            set
            {
                _artistId = value;
                RaisePropertyChanged(nameof(ArtistId));
            }
        }
        public int SizeX { get; } = 250;
    }
}
