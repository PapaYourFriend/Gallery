using Gallery.Core.DomainModels;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Gallery.ViewModel
{
    public class GalleryViewModel : BaseViewModel
    {
        private List<PictureModel> _pictures;
        private List<PictureModel> _fullPictures;
        private readonly MainWindowViewModel _mainWindow;
        private RelayCommand _buyCommand;
        private RelayCommand _searchCommand;
        private RelayCommand _upCommand;
        private RelayCommand _downCommand;
        private PictureModel _selectedItem;
        private bool _dropopenstyle = false;
        private string _text;
        private bool priceFilter = true;

        private ObservableCollection<Item> _mStyles;
        private HashSet<Item> _mCheckedStyles;
        public IEnumerable<Item> Styles { get { return _mStyles; } }

        public GalleryViewModel(MainWindowViewModel mainWindow)
        {
            _mainWindow = mainWindow;
            Pictures = new List<PictureModel>(_mainWindow.GalleryService.GetAllPictures());
            _fullPictures = new List<PictureModel>(_pictures);
            _mStyles = new ObservableCollection<Item>();
            _mCheckedStyles = new HashSet<Item>();
            _mStyles.CollectionChanged += Style_CollectionChanged;

            foreach(var item in _mainWindow.GalleryService.GetAllStyles())
            {
                _mStyles.Add(item);
            }
        }

        public bool PriceFilter
        {
            get { return priceFilter; }
            set
            {
                priceFilter = value;
                OnPropertyChanged(nameof(PriceFilter));
            }
        }

        public List<PictureModel> Pictures
        {
            get { return _pictures; }
            set
            {
                _pictures = value;
                OnPropertyChanged(nameof(Pictures));
            }
        }

        public PictureModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                if (value != null)
                {
                    _mainWindow.CurrentViewModel = new PictureViewModel(_mainWindow, this, _selectedItem);
                }
            }
        }
        public RelayCommand BuyCommand
        {
            get
            {
                return _buyCommand ??
                    (_buyCommand = new RelayCommand(async (obj) =>
                    {
                        var p = obj as PictureModel;
                        if(CurrentUser.ProfileId is null)
                        {
                            _mainWindow.CurrentViewModel = new AuthorizationViewModel(this, _mainWindow);
                        }
                        else
                        {
                            var profile = _mainWindow.OrderService.GetProfileForOrder((int)CurrentUser.ProfileId);
                            var orderDTO = new OrderDTO(p, profile);
                            DialogViewModel dialog = new DialogViewModel(new OrderViewModel(ref orderDTO));
                            bool result = (bool)_mainWindow.DialogService.ShowDialog("Order", dialog);
                            if(result)
                            {
                                try
                                {
                                    await _mainWindow.OrderService.MakeOrderAsync(orderDTO, profile, _mainWindow.MailService);
                                    _mainWindow.MessageBoxService.ShowMessageBox(
                                    "Заказ сделан!",
                                    "Order",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                                }
                                catch(ArgumentException ex)
                                {
                                    _mainWindow.MessageBoxService.ShowMessageBox(
                                    ex.Message,
                                    "Order",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                                }
                                
                            }
                        }
                    }));
            }
        }


        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ??
                    (_searchCommand = new RelayCommand((obj) =>
                    {
                        var tmp = new List<PictureModel>();
                        foreach(var item in _mCheckedStyles)
                        {
                            tmp.AddRange(_fullPictures.Where(p => p.StyleName == item.Style));
                        }
                        if(_mCheckedStyles.Count > 0)
                        {
                            Pictures = tmp;
                        }
                        else
                        {
                            Pictures = _fullPictures;
                        }
                    }));
            }
        }

        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand ??
                    (_upCommand = new RelayCommand((obj) =>
                    {
                        Pictures = new List<PictureModel>(Pictures.OrderBy(p => p.Price));
                        PriceFilter = false;
                    }));
            }
        }

        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand ??
                    (_downCommand = new RelayCommand((obj) =>
                    {
                        Pictures = new List<PictureModel>(Pictures.OrderByDescending(p => p.Price));
                        PriceFilter = true;
                    }));
            }
        }



        public bool DropOpenStyle
        {
            get { return _dropopenstyle; }
            set { _dropopenstyle = value; OnPropertyChanged(nameof(Text)); }
        }
        
        public string Text
        {
            set
            {
                if (value != null && value.IndexOf("Item") != -1) return;
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
            get
            {
                return _text;
            }
        }

        private void Style_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Style_PropertyChanged;
                    _mCheckedStyles.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Style_PropertyChanged;
                    if (item.IsChecked) _mCheckedStyles.Add(item);
                }
            }
            UpdateText();
        }

        private void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    _mCheckedStyles.Add(item);
                }
                else
                {
                    _mCheckedStyles.Remove(item);
                }
                UpdateText();
            }
        }

        private void UpdateText()
        {
            switch (_mCheckedStyles.Count)
            {
                case 0:
                    Text = "Стиль картины:";
                    break;
                case 1:
                    Text = _mCheckedStyles.First().Style;
                    break;
                default:
                    Text = "Несколько";
                    break;
            }
        }
    }
}
