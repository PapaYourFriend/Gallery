using Gallery.ViewModel;
using System.Windows;

namespace Gallery.View.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(AdminViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
