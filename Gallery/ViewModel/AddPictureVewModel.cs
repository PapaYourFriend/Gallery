using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.IO;
using System.Windows.Input;

namespace Gallery.ViewModel
{
    public class AddPictureVewModel : BaseViewModel, IDialogResultVMHelper
    {
        private readonly Lazy<DelegateCommand> _okCommand;
        private readonly IFileUploadingService _service;
        private readonly IMessageBoxService _messageBox;
        private RelayCommand _addPicCommand;

        public AddPictureVewModel(ref PictureDTO pictureDTO,
            IFileUploadingService service,
            IMessageBoxService messageBox)
        {
            this._okCommand = new Lazy<DelegateCommand>(() =>
                new DelegateCommand(() =>
                    InvokeRequestCloseDialog(
                        new RequestCloseDialogEventArgs(true)), () => !NewPic.HasErrors));
            NewPic = pictureDTO;
            _service = service;
            _messageBox = messageBox;
        }
        public ICommand OkCommand
        {
            get
            {
                return this._okCommand.Value;
            }
        }
        public PictureDTO NewPic { get; set; }

        public RelayCommand AddPicCommand
        {
            get
            {
                return _addPicCommand ??
                    (_addPicCommand = new RelayCommand(async (obj) =>
                    {
                        var path = _service.OpenFileDialog();
                        if (!string.IsNullOrEmpty(path))
                        {
                            try
                            {
                                var endPath = await _service.AddPictureDataAsync(path);
                                NewPic.DataPath = endPath;
                            }
                            catch (IOException ex)
                            {
                                _messageBox.ShowMessageBox(
                                    ex.Message,
                                    "Picture",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                            }

                        }
                    }));
            }
        }






        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
        private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
        {
            var handler = RequestCloseDialog;
            if (handler != null)
                handler(this, e);
        }
    }
}
