using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gallery.ViewModel.Helpers
{
    public class Item : INotifyPropertyChanged
    {
        public string Style { get; private set; }
        public string Status { get; set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        private bool _isChecked;

        public Item(string Style)
        {
            this.Style = Style;
        }
        public Item() { }






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
