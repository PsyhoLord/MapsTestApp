using MapsTestApp.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace MapsTestApp
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Provider")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDbService, DbService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IRequestService, RequestService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ILocationService, LocationService>();

            // register the appstart object
            RegisterCustomAppStart<AppStart>();
        }
    }
}
