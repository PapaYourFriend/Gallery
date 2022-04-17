using Gallery.Core.DomainModels;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Gallery.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _mainWindow;
        private ObservableCollection<OrderModel> _orders;
        private ObservableCollection<OrderModel> _ordersEnded;
        private ProfileModel _user;
        private OrderModel _selectedActiveOrder;
        private OrderModel _selectedEndedOrder;
        private RelayCommand _logOutCommand;
        private RelayCommand _updateProfileCommand;
        private RelayCommand _deleteProfileCommand;
        private RelayCommand _changePicCommand;
        private RelayCommand _deleteOrderCommand;

        public ProfileViewModel(MainWindowViewModel mainWindow)
        {
            _mainWindow = mainWindow;

            Orders = _mainWindow.OrderService.GetAllUserActiveOrders((int)CurrentUser.ProfileId);

            OrdersEnded = _mainWindow.OrderService.GetAllUserEndedOrders((int)CurrentUser.ProfileId);

            User = _mainWindow.AuthenticationService.GetUserByIdOrDefault((int)CurrentUser.ProfileId);

        }

        public OrderModel SelectedActiveOrder
        {
            get { return _selectedActiveOrder; }
            set
            {
                _selectedActiveOrder = value;
                OnPropertyChanged(nameof(SelectedActiveOrder));
                if(value != null)
                {
                    var model = _mainWindow.GalleryService.GetPictureById(_selectedActiveOrder.PictureId);
                    _mainWindow.CurrentViewModel = new PictureViewModel(_mainWindow, this, model);
                }
            }
        }

        public OrderModel SelectedEndedOrder
        {
            get { return _selectedEndedOrder; }
            set
            {
                _selectedEndedOrder = value;
                OnPropertyChanged(nameof(SelectedEndedOrder));
                if (value != null)
                {
                    var model = _mainWindow.GalleryService.GetPictureById(_selectedEndedOrder.PictureId);
                    _mainWindow.CurrentViewModel = new PictureViewModel(_mainWindow, this, model);
                }
            }
        }


        public ObservableCollection<OrderModel> OrdersEnded
        {
            get { return _ordersEnded; }
            set
            {
                _ordersEnded = value;
                OnPropertyChanged(nameof(OrdersEnded));
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

        public ProfileModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public RelayCommand LogOutCommand
        {
            get
            {
                return _logOutCommand ??
                    (_logOutCommand = new RelayCommand((obj) =>
                    {
                        if(_mainWindow.MessageBoxService.ShowMessageBox(
                                    "Хотите выйти?",
                                    "Logout",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {
                            _mainWindow.DataPath = null;
                            _mainWindow.ProfileId = null;
                            CurrentUser.ProfileId = null;
                            CurrentUser.DataPath = null;
                            _mainWindow.CurrentViewModel = new GalleryViewModel(_mainWindow);
                        }
                    }));
            }
        }

        public RelayCommand DeleteProfileCommand
        {
            get
            {
                return _deleteProfileCommand ??
                    (_deleteProfileCommand = new RelayCommand((obj) =>
                    {
                        if(_mainWindow.MessageBoxService.ShowMessageBox(
                                    "Хотите удалить аккаунт? Ваши заказы будут отменены!",
                                    "Delete",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {

                            _mainWindow.AuthenticationService.DeleteAccount((int)CurrentUser.ProfileId);
                            _mainWindow.DataPath = null;
                            _mainWindow.ProfileId = null;
                            CurrentUser.ProfileId = null;
                            CurrentUser.DataPath = null;
                            _mainWindow.CurrentViewModel = new GalleryViewModel(_mainWindow);
                        }
                    }));
            }
        }

        public RelayCommand UpdateProfileCommand
        {
            get
            {
                return _updateProfileCommand ??
                    (_updateProfileCommand = new RelayCommand((obj) =>
                    {
                        _mainWindow.AuthenticationService.UpdateAccount(User);
                        _mainWindow.DataPath = User.DataPath;
                    },(obj) => !User.HasErrors));
            }
        }

        public RelayCommand ChangePicCommand
        {
            get
            {
                return _changePicCommand ??
                    (_changePicCommand = new RelayCommand(async (obj) =>
                    {
                        var path = _mainWindow.FileUploadingService.OpenFileDialog();
                        if(!string.IsNullOrEmpty(path))
                        {
                            try
                            {
                                var endPath = await _mainWindow.FileUploadingService.AddProfileDataAsync(path, (int)CurrentUser.ProfileId);
                                User.DataPath = endPath;
                            }
                            catch(IOException ex)
                            {
                                _mainWindow.MessageBoxService.ShowMessageBox(
                                    ex.Message,
                                    "Profile",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
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
                        var o = obj as OrderModel;
                        if(o != null)
                        {
                            if(_mainWindow.MessageBoxService.ShowMessageBox(
                                    "Хотите отменить заказ?",
                                    "Abandon",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                            {
                                await _mainWindow.OrderService.DeleteOrderAsync(o, _mainWindow.MailService);

                                Orders.Remove(o);
                                OrdersEnded.Add(o);
                            }
                            
                        }
                    }));
            }
        }
    }
}