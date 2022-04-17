using Gallery.ServiceLayer.Interfaces;
using Gallery.View.Windows;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;

namespace Gallery.ViewModel
{
    public class AuthorizationViewModel : BaseViewModel
    {
        private readonly BaseViewModel _currentModel;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private RelayCommand _registerCommand;
        private RelayCommand _loginCommand;

        public AuthorizationViewModel(BaseViewModel currentModel,
            MainWindowViewModel mainWindowViewModel)
        {
            _currentModel = currentModel;
            _mainWindowViewModel = mainWindowViewModel;

        }
        public RegisterDTO RegisterDTO { get; set; } = new RegisterDTO();
        public LoginDTO LoginDTO { get; set; } = new LoginDTO();


        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ??
                    (_registerCommand = new RelayCommand((obj) =>
                    {
                        if (!string.IsNullOrEmpty(RegisterDTO.Email) && !string.IsNullOrEmpty(RegisterDTO.Name) && !string.IsNullOrEmpty(RegisterDTO.Password))
                        {
                            var result = _mainWindowViewModel.AuthenticationService.Register(RegisterDTO);

                            _mainWindowViewModel.MessageBoxService.ShowMessageBox(
                                    result,
                                    "Register",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                        }

                    }, (obj) => !RegisterDTO.HasErrors));
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand((obj) =>
                    {
                        bool admin = false;
                        var result = _mainWindowViewModel.AuthenticationService.Login(LoginDTO, ref admin);

                        _mainWindowViewModel.MessageBoxService.ShowMessageBox(
                                result,
                                "Login",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Information);

                        if (CurrentUser.ProfileId != null)
                        {
                            _mainWindowViewModel.ProfileId = CurrentUser.ProfileId;
                            _mainWindowViewModel.DataPath = CurrentUser.DataPath;
                            _mainWindowViewModel.CurrentViewModel = _currentModel;
                        }
                        if(admin)
                        {
                            AdminWindow adminWindow = new AdminWindow(new AdminViewModel(_mainWindowViewModel));
                            adminWindow.Show();
                            _mainWindowViewModel.CloseAction();
                        }

                    }, (obj) => !LoginDTO.HasErrors));
            }
        }
    }
}

