using System;

namespace Gallery.ViewModel.Helpers
{
    public interface IDialogResultVMHelper
    {
        event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
    }
}
