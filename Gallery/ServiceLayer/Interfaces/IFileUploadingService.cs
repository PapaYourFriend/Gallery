using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IFileUploadingService
    {
        string OpenFileDialog();
        Task<string> AddPictureDataAsync(string filePath);
        Task<string> AddProfileDataAsync(string filePath, int profileId);
    }
}
