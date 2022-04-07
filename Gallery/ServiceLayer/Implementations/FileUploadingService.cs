using Gallery.ServiceLayer.Interfaces;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Implementations
{
    public class FileUploadingService : IFileUploadingService
    {
        private readonly string _usersDataFolder;
        private readonly string _picturesDataFolder;
        public FileUploadingService()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            _usersDataFolder = settings["userDataFolder"].Value;
            _picturesDataFolder = settings["picturesDataFolder"].Value;
        }

        public async Task<string> AddPictureDataAsync(string filePath)
        {
            byte[] data;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                data = new byte[file.Length];
                await file.ReadAsync(data, 0, data.Length);
            }
            var imageName = filePath.Substring(filePath.LastIndexOf(@"\"));

            DirectoryInfo info = new DirectoryInfo(_picturesDataFolder);

            if (!info.Exists)
            {
                info.Create();
            }

            var pathToAdd = info.FullName + imageName;

            using (FileStream file = new FileStream(pathToAdd, FileMode.Create))
            {
                await file.WriteAsync(data, 0, data.Length);
            }

            return pathToAdd;
        }

        public async Task<string> AddProfileDataAsync(string filePath, int profileId)
        {
            byte[] data;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                data = new byte[file.Length];
                await file.ReadAsync(data, 0, data.Length);
            }
            var imageName = filePath.Substring(filePath.LastIndexOf(@"\"));

            var userFolder = _usersDataFolder + $@"\{profileId}";

            DirectoryInfo info = new DirectoryInfo(userFolder);
            
            if(!info.Exists)
            {
                info.Create();
            }

            var pathToAdd = info.FullName + imageName;

            using (FileStream file = new FileStream(pathToAdd, FileMode.Create))
            {
                await file.WriteAsync(data, 0, data.Length);
            }

            return pathToAdd;
        }

        public string OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            bool? result = openFileDialog.ShowDialog();
            var file_path = openFileDialog.FileName;
            return file_path;
        }
    }
}
