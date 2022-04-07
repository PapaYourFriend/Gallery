using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
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
    public class AdminViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IMailService _mailService;
        private readonly IGalleryService _galleryService;
        private readonly IReviewService _reviewService;
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        private readonly IFileUploadingService _fileService;
        private ObservableCollection<Profile> _users;
        private List<Profile> _usersFull;
        private List<OrderModel> _ordersFull;
        private List<PictureModel> _picturesFull;
        private ObservableCollection<OrderModel> _orders;
        private ObservableCollection<PictureModel> _pictures;
        private ObservableCollection<ReviewModel> _reviews;
        private RelayCommand _deleteCommand;
        private RelayCommand _deleteOrderCommand;
        private RelayCommand _searchCommand;
        private RelayCommand _addPictureCommand;
        private RelayCommand _deletePictureCommand;
        private RelayCommand _deleteReviewCommand;
        private string _searchUser;
        private string _searchPicture;

        private bool _dropopenstatus = false;
        private string _text;

        private ObservableCollection<Item> _mStatuses;
        private HashSet<Item> _mCheckedStatuses;
        public IEnumerable<Item> Statuses { get { return _mStatuses; } }

        public AdminViewModel(IAuthenticationService authenticationService,
            IOrderService orderService,
            IMessageBoxService messageBoxService,
            IMailService mailService,
            IGalleryService galleryService,
            IReviewService reviewService,
            IDialogService dialogService,
            IFileUploadingService fileService)
        {
            _authenticationService = authenticationService;
            _orderService = orderService;
            _messageBoxService = messageBoxService;
            _mailService = mailService;
            _galleryService = galleryService;
            _reviewService = reviewService;
            _dialogService = dialogService;
            _fileService = fileService;
            Users = new ObservableCollection<Profile>(_authenticationService.GetAllUsers());
            Orders = new ObservableCollection<OrderModel>(_orderService.GetAllOrderModels());
            Pictures = new ObservableCollection<PictureModel>(_galleryService.GetAllPictures());
            Reviews = new ObservableCollection<ReviewModel>(_reviewService.GetAllReviews());

            _ordersFull = new List<OrderModel>(Orders);
            _usersFull = new List<Profile>(Users);
            _picturesFull = new List<PictureModel>(Pictures);

            _mStatuses = new ObservableCollection<Item>();
            _mCheckedStatuses = new HashSet<Item>();
            _mStatuses.CollectionChanged += Status_CollectionChanged;

            foreach (var item in _orderService.GetAllOrdersStatuses())
            {
                _mStatuses.Add(item);
            }


        }

        public string SearchUser
        {
            get { return _searchUser; } 
            set
            {
                _searchUser = value;
                OnPropertyChanged(nameof(SearchUser));
                if(!string.IsNullOrEmpty(_searchUser))
                {
                    var tmp = new List<Profile>();

                    tmp.AddRange(_usersFull.Where(u => u.ProfileSecret.EmailLogin.Contains(_searchUser)));

                    Users = new ObservableCollection<Profile>(tmp);
                }
                else
                {
                    Users = new ObservableCollection<Profile>(_usersFull);
                }
            }
        }
        public string SearchPicture
        {
            get { return _searchPicture; } 
            set
            {
                _searchPicture = value;
                OnPropertyChanged(nameof(SearchPicture));
                if(!string.IsNullOrEmpty(_searchPicture))
                {
                    var tmp = new List<PictureModel>();

                    tmp.AddRange(_picturesFull.Where(p => p.PictureName.Contains(_searchPicture)));

                    Pictures = new ObservableCollection<PictureModel>(tmp);
                }
                else
                {
                    Pictures = new ObservableCollection<PictureModel>(_picturesFull);
                }
            }
        }


        public ObservableCollection<Profile> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<ReviewModel> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

        public ObservableCollection<PictureModel> Pictures
        {
            get { return _pictures; }
            set
            {
                _pictures = value;
                OnPropertyChanged(nameof(Pictures));
            }
        }

        public ObservableCollection<OrderModel> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }


        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand((obj) =>
                    {
                        var user = obj as Profile;
                        if (_messageBoxService.ShowMessageBox(
                                    "Хотите удалить аккаунт?",
                                    "Delete",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {

                            if(user != null)
                            {
                                _authenticationService.DeleteAccount(user.ProfileId);
                                Users.Remove(user);
                            }
                            
                        }
                    }));
            }
        }
        
        public RelayCommand DeleteOrderCommand
        {
            get
            {
                return _deleteOrderCommand ??
                    (_deleteOrderCommand = new RelayCommand(async (obj) =>
                    {
                        var order = obj as OrderModel;
                        if (_messageBoxService.ShowMessageBox(
                                    "Хотите отменить заказ?",
                                    "Abandon",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {

                            if(order != null)
                            {
                                await _orderService.DeleteOrderAsync(order, _mailService);
                                Orders.Remove(order);
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
                        var tmp = new List<OrderModel>();
                        foreach (var item in _mCheckedStatuses)
                        {
                            tmp.AddRange(_ordersFull.Where(p => p.OrderStatusName == item.Status));
                        }
                        if (_mCheckedStatuses.Count > 0)
                        {
                            Orders = new ObservableCollection<OrderModel>(tmp);
                        }
                        else
                        {
                            Orders = new ObservableCollection<OrderModel>(_ordersFull);
                        }
                    }));
            }
        }

        public RelayCommand DeleteReviewCommand
        {
            get
            {
                return _deleteReviewCommand ??
                    (_deleteReviewCommand = new RelayCommand((obj) =>
                    {
                        var review = obj as ReviewModel;
                        if (_messageBoxService.ShowMessageBox(
                                    "Хотите удалить отзыв?",
                                    "Delete",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {

                            if(review != null)
                            {
                                _reviewService.RemoveReview(review);
                                Reviews.Remove(review);
                            }
                            
                        }
                    }));
            }
        }

        public RelayCommand AddPictureCommand
        {
            get
            {
                return _addPictureCommand ??
                    (_addPictureCommand = new RelayCommand((obj) =>
                    {
                        var addPic = new PictureDTO();
                        DialogViewModel dialog = new DialogViewModel(new AddPictureVewModel(ref addPic, _fileService));
                        bool result = (bool)_dialogService.ShowDialog("Picture", dialog);
                        if(result)
                        {
                            try
                            {
                                var newModel = _galleryService.AddPicture(addPic);
                                Pictures.Add(newModel);
                            }
                            catch(ArgumentNullException ex)
                            {
                                _messageBoxService.ShowMessageBox(
                                    ex.Message,
                                    "Error",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                            }
                           
                        }
                    }));
            }
        }

        public RelayCommand DeletePictureCommand
        {
            get
            {
                return _deletePictureCommand ??
                    (_deletePictureCommand = new RelayCommand((obj) =>
                    {
                        var pic = obj as PictureModel;
                        if(pic != null)
                        {
                            if(_messageBoxService.ShowMessageBox(
                                    "Хотите удалить картину?",
                                    "Error",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question)) 
                            {
                                if(_galleryService.DeletePicture(pic.PictureId))
                                {
                                    Pictures.Remove(pic);
                                }
                                else
                                {
                                    _messageBoxService.ShowMessageBox(
                                    "Вы не можете удалить эту картину(она есть в заказах)!",
                                    "Error",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                                }
                            }
                            
                        }

                    }));
            }
        }



        public bool DropOpenStatus
        {
            get { return _dropopenstatus; }
            set { _dropopenstatus = value; OnPropertyChanged(nameof(Text)); }
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

        private void Status_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Status_PropertyChanged;
                    _mCheckedStatuses.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Status_PropertyChanged;
                    if (item.IsChecked) _mCheckedStatuses.Add(item);
                }
            }
            UpdateText();
        }

        private void Status_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    _mCheckedStatuses.Add(item);
                }
                else
                {
                    _mCheckedStatuses.Remove(item);
                }
                UpdateText();
            }
        }

        private void UpdateText()
        {
            switch (_mCheckedStatuses.Count)
            {
                case 0:
                    Text = "Статус заказа:";
                    break;
                case 1:
                    Text = _mCheckedStatuses.First().Status;
                    break;
                default:
                    Text = "Несколько";
                    break;
            }
        }
    }
}
