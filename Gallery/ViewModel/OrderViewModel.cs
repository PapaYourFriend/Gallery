using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Windows.Input;

namespace Gallery.ViewModel
{
    public class OrderViewModel : BaseViewModel, IDialogResultVMHelper
    {
        private readonly Lazy<DelegateCommand> _okCommand;

        public OrderViewModel(ref OrderDTO orderDTO)
        {
            this._okCommand = new Lazy<DelegateCommand>(() =>
                new DelegateCommand(() =>
                    InvokeRequestCloseDialog(
                        new RequestCloseDialogEventArgs(true)), () => !OrderDTO.HasErrors));
            OrderDTO = orderDTO;
        }
        public ICommand OkCommand
        {
            get
            {
                return this._okCommand.Value;
            }
        }
        public OrderDTO OrderDTO { get; set; }




        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
        private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
        {
            var handler = RequestCloseDialog;
            if (handler != null)
                handler(this, e);
        }
    }
}
