using Gallery.ServiceLayer.Interfaces;
using System.Windows.Forms;

namespace Gallery.ServiceLayer.Implementations
{
    public class MessageBoxService : IMessageBoxService
    {
        public bool ShowMessageBox(string text, string caption, MessageBoxButtons button, MessageBoxIcon image)
        {
            DialogResult result = MessageBox.Show(text, caption, button, image);
            return DialogResult.Yes == result;
        }
    }
}
