using Gallery.Core.DataAccess.Implementation;
using Gallery.Core.DataAccess.Interfaces;
using Gallery.ServiceLayer.Implementations;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel;
using Ninject.Modules;

namespace Gallery
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            //Services
            Bind<IGalleryService>().To<GalleryService>().InTransientScope();
            Bind<IAuthenticationService>().To<AuthenticationService>().InTransientScope();
            Bind<IDialogService>().To<DialogService>().InTransientScope();
            Bind<IOrderService>().To<OrderService>().InTransientScope();
            Bind<IReviewService>().To<ReviewService>().InTransientScope();
            Bind<IMessageBoxService>().To<MessageBoxService>().InTransientScope();
            Bind<IMailService>().To<MailService>().InSingletonScope();
            Bind<IFileUploadingService>().To<FileUploadingService>().InSingletonScope();
            //DataAccess
            Bind<IPicturesRepository>().To<PicturesRepository>().InTransientScope();
            Bind<IOrderRepository>().To<OrderRepository>().InTransientScope();
            Bind<IReviewRepository>().To<ReviewRepository>().InTransientScope();
            Bind<IProfileRepository>().To<ProfileRepository>().InTransientScope();
            Bind<IArtistRepository>().To<ArtistRepository>().InTransientScope();
            Bind<IGalleryDbContext>().To<GalleryDbContext>().InSingletonScope();

            //ViewModels
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            Bind<AdminViewModel>().ToSelf().InTransientScope();
        }
    }
}
