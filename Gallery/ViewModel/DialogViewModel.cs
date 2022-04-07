using Gallery.ViewModel.Common;

namespace Gallery.ViewModel
{
    public class DialogViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }
        public DialogViewModel(BaseViewModel vm)
        {
            CurrentViewModel = vm;
        }
    }
}
