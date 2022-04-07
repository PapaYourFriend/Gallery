using System.Windows.Forms;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IMessageBoxService
    {
        bool ShowMessageBox(string text, string caption, MessageBoxButtons button, MessageBoxIcon image);
    }
}
