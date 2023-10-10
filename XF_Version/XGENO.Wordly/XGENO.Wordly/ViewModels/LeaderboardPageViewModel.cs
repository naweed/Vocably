using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class LeaderboardPageViewModel : AppViewModelBase
    {
        public LeaderboardPageViewModel() : base()
        {
            this.Title = "LEADERBOARD";
        }

        public override async void OnNavigatedTo(object parameters)
        {
        }

    }
}
