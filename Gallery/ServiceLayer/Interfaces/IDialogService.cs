namespace Gallery.ServiceLayer.Interfaces
{
    public interface IDialogService
    {
        bool? ShowDialog(string title, object DataContext);
    }
}
