using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class PrivacyPolicyPageViewModel : AppViewModelBase
    {
        public PrivacyPolicyPageViewModel() : base()
        {
            this.Title = "PRIVACY POLICY";
        }

        public override async void OnNavigatedTo(object parameters)
        {
        }

    }
}
