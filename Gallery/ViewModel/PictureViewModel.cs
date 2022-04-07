using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System;

namespace Gallery.ViewModel
{
    public class PictureViewModel : BaseViewModel
    {
        private PictureModel _selectedItem;
        private readonly MainWindowViewModel _main;
        private readonly BaseViewModel _model;
        private RelayCommand _backCommand;
        private RelayCommand _profileCommand;
        private RelayCommand _buyCommand;

        public PictureViewModel(MainWindowViewModel main,
            BaseViewModel model, 
            PictureModel selectedItem
            )
        {
            _main = main;
            _model = model;
            SelectedItem = selectedItem;
            OrderAction = _main.OrderService.GetOrderByPictureId(SelectedItem.PictureId);
        }

        public OrderAction OrderAction { get; set; }

        public PictureModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ??
                    (_backCommand = new RelayCommand((obj) =>
                    {
                        if(_model is GalleryViewModel)
                        {
                            ((GalleryViewModel)_model).SelectedItem = null;
                        }
                        if(_model is ProfileViewModel)
                        {
                            ((ProfileViewModel)_model).SelectedActiveOrder = null;
                            ((ProfileViewModel)_model).SelectedEndedOrder = null;
                        }
                        _main.CurrentViewModel = _model;
                    }));
            }
        }
        
        public RelayCommand ProfileCommand
        {
            get
            {
                return _profileCommand ??
                    (_profileCommand = new RelayCommand((obj) =>
                    {
                        if(_model is ProfileViewModel)
                        {
                            _main.CurrentViewModel = _model;
                        }
                        else
                        {
                            _main.CurrentViewModel = new ProfileViewModel(_main);
                        }
                    }));
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
                        if (CurrentUser.ProfileId is null)
                        {
                            _main.CurrentViewModel = new AuthorizationViewModel(this, _main);
                        }
                        else
                        {
                            var profile = _main.OrderService.GetProfileForOrder((int)CurrentUser.ProfileId);
                            var orderDTO = new OrderDTO(p, profile);
                            DialogViewModel dialog = new DialogViewModel(new OrderViewModel(ref orderDTO));
                            bool result = (bool)_main.DialogService.ShowDialog("Order", dialog);
                            if (result)
                            {
                                try
                                {
                                    await _main.OrderService.MakeOrderAsync(orderDTO, profile, _main.MailService);
                                    _main.MessageBoxService.ShowMessageBox(
                                    "Заказ сделан!",
                                    "Order",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                                }
                                catch (ArgumentException ex)
                                {
                                    _main.MessageBoxService.ShowMessageBox(
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
    }
}
