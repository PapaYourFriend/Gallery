using Gallery.ServiceLayer.Interfaces;
using Gallery.View.Windows;

namespace Gallery.ServiceLayer.Implementations
{
    public class DialogService : IDialogService
    {
        public bool? ShowDialog(string title, object DataContext)
        {
            var win = new DialogWindow();
            win.Title = title;
            win.DataContext = DataContext;
            return win.ShowDialog();
        }
    }
}
