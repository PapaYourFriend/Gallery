using Gallery.View.Windows;
using Ninject;
using System.Windows;

namespace Gallery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _iocKernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _iocKernel = new StandardKernel();
            _iocKernel.Load(new IocConfiguration());

            Current.MainWindow = _iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}
