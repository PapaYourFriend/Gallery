using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.Helpers;
using System;
using System.Windows.Controls;

namespace Gallery.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;
        private readonly AdminViewModel _adminViewModel;
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IMailService _mailService;
        private readonly IFileUploadingService _fileUploadingService;
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private int? _profileId = CurrentUser.ProfileId;
        private string _dataPath = CurrentUser.DataPath;
        private BaseViewModel _currentViewModel;
        public Action CloseAction { get; set; }


        public RelayCommand SelectCommand { get; set; }
        public MainWindowViewModel(IGalleryService galleryService,
            IDialogService dialogService,
            IAuthenticationService authenticationService,
            IMessageBoxService messageBoxService,
            IOrderService orderService,
            IMailService mailService,
            IFileUploadingService fileUploadingService,
            IReviewService reviewService,
            AdminViewModel adminViewModel)
        {
            _galleryService = galleryService;
            _dialogService = dialogService;
            _authenticationService = authenticationService;
            _messageBoxService = messageBoxService;
            _orderService = orderService;
            _mailService = mailService;
            _fileUploadingService = fileUploadingService;
            _reviewService = reviewService;
            _adminViewModel = adminViewModel;


            CurrentViewModel = new GalleryViewModel(this);
            SelectCommand = new RelayCommand((selectedItem) =>
            {
                ListBoxItem item = selectedItem as ListBoxItem;
                if (item != null)
                {
                    switch (item.Name)
                    {
                        case "main":
                            CurrentViewModel = new GalleryViewModel(this);
                            break;
                        case "reviews":
                            CurrentViewModel = new ReviewsViewModel(this);
                            break;
                        case "login":
                            CurrentViewModel = new AuthorizationViewModel(CurrentViewModel, this);
                            break;
                        case "profile":
                            CurrentViewModel = new ProfileViewModel(this);
                            break;
                    }
                }
            });

            OrderMonitoring();
        }
        public int? ProfileId
        {
            get { return _profileId; }
            set
            {
                _profileId = value;
                OnPropertyChanged(nameof(ProfileId));
            }
        }

        public string DataPath
        {
            get { return _dataPath; }
            set
            {
                _dataPath = value;
                OnPropertyChanged(nameof(DataPath));
            }
        }
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public IGalleryService GalleryService
        {
            get { return _galleryService; }
        }

        public IDialogService DialogService
        {
            get { return _dialogService; }
        }

        public IAuthenticationService AuthenticationService
        {
            get { return _authenticationService; }
        }

        public IMessageBoxService MessageBoxService
        {
            get { return _messageBoxService; }
        }

        public IOrderService OrderService
        {
            get { return _orderService; }
        }

        public IMailService MailService
        {
            get { return _mailService; }
        }
        public IFileUploadingService FileUploadingService
        {
            get { return _fileUploadingService; }
        }
        public IReviewService ReviewService
        {
            get { return _reviewService; }
        }
        public AdminViewModel AdminViewModel
        {
            get { return _adminViewModel; }
        }




        private void OrderMonitoring()
        {
            var orders = _orderService.GetAllOrders();
            
            foreach (var order in orders)
            {
                if (order.OrderStatusId != (int)OrderStatuses.Abandoned && order.OrderStatusId != (int)OrderStatuses.Accepted)
                {
                    if (DateTime.Now.Day - order.CreatedAt.Day == (int)OrderStatuses.InProcessing)
                    {
                        order.OrderStatusId = (int)OrderStatuses.Packaging;
                    }
                    else if (DateTime.Now.Day - order.CreatedAt.Day == (int)OrderStatuses.Packaging)
                    {
                        order.OrderStatusId = (int)OrderStatuses.OnTheWay;
                    }
                    else if (DateTime.Now.Day - order.CreatedAt.Day >= (int)OrderStatuses.OnTheWay)
                    {
                        order.OrderStatusId = (int)OrderStatuses.Accepted;
                    }
                }
            }

            _orderService.UpdateOrders(orders);
        }
    }
}
