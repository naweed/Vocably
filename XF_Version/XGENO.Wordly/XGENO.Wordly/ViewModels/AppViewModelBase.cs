using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Mobile.Framework.Services;
using XGENO.Wordly.Interfaces;

namespace XGENO.Wordly.ViewModels
{
    public class AppViewModelBase : ViewModelBase
    {
        public INavigation NavigationService { get; set; }
        protected IApiService _appApiService { get; set; }
        protected ISettingsService _appSettingsService { get; set; }
        protected IDatabaseService _appDBService { get; set; }
        public Page PageService { get; set; }

        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        public AppViewModelBase() : base()
        {
            _appApiService = ContainerService.GetSingleton<IApiService>();
            _appSettingsService = ContainerService.GetSingleton<ISettingsService>();
            _appDBService = ContainerService.GetSingleton<IDatabaseService>();

            //Initialize Commands
            NavigateBackCommand = new DelegateCommand(NavigateBack);
            CloseCommand = new DelegateCommand(CloseModal);
        }

        private async Task NavigateBack() => await NavigationService.PopAsync();

        private async Task CloseModal() => await NavigationService.PopModalAsync();

        //Called on Page Appearing
        public virtual async void OnNavigatedTo(object parameters) => await Task.CompletedTask;
    }
}
