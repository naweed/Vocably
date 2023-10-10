using MonkeyCache.FileStore;
using Xamarin.Essentials;
using Xamarin.Forms;
using XGENO.Mobile.Framework.Services;
using XGENO.Wordly.Interfaces;
using XGENO.Wordly.Models;
using XGENO.Wordly.Services;
using XGENO.Wordly.ViewModels;
using XGENO.Wordly.Views;

//Register Fonts
[assembly: ExportFont("FiraSans-Light.ttf", Alias = "LightFont")]
[assembly: ExportFont("FiraSans-Medium.ttf", Alias = "MediumFont")]
[assembly: ExportFont("FiraSans-Regular.ttf", Alias = "RegularFont")]
[assembly: ExportFont("FiraSans-SemiBold.ttf", Alias = "BoldFont")]
[assembly: ExportFont("FiraSansCondensed-Regular.ttf", Alias = "ThinFont")]
[assembly: ExportFont("BarlowCondensed-Medium.ttf", Alias = "MediumCondensedFont")]
namespace XGENO.Wordly
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Enable Version Tracking
            VersionTracking.Track();

            //Register Cache Barrel
            Barrel.ApplicationId = Constants.ApplicationId;

            //Register Services
            RegisterServices();

            MainPage = new NavigationPage(new StartPage());
        }

        private static void RegisterServices()
        {
            //App API Service
            var appApiService = new WordsApiService(Constants.ApiServiceURL);
            appApiService.SetCacheBarrel(Barrel.Current);

            ContainerService.RegisterSingleton<IApiService>(appApiService); //API Service
            ContainerService.RegisterSingleton<ISettingsService>(new AppSettingsService()); //Settings Service
            ContainerService.RegisterSingleton<IDatabaseService>(new AppDBService()); //Database Service

            //View Models
            ContainerService.RegisterType<StartPageViewModel>();
            ContainerService.RegisterType<StatsPageViewModel>();
            ContainerService.RegisterType<AboutUsPageViewModel>();
            ContainerService.RegisterType<PrivacyPolicyPageViewModel>();
            ContainerService.RegisterType<NewGamePageViewModel>();
            ContainerService.RegisterType<RulesPageViewModel>();
            ContainerService.RegisterType<LeaderboardPageViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
