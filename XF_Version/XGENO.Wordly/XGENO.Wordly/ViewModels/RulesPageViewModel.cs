using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class RulesPageViewModel : AppViewModelBase
    {
        public RulesPageViewModel() : base()
        {
            this.Title = "GAME RULES";
        }

        public override async void OnNavigatedTo(object parameters)
        {
            //Marks rules as viewed
            if(!_appSettingsService.RulesViewed)
                _appSettingsService.RulesViewed = true;
        }
    }
}
