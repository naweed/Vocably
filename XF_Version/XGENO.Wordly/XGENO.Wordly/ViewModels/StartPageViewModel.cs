using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.Exceptions;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Helpers;
using XGENO.Wordly.Models;
using XGENO.Wordly.Views;

namespace XGENO.Wordly.ViewModels
{
    public class StartPageViewModel : AppViewModelBase
    {
        private string _gameProgressDisplay;
        public string GameProgressDisplay
        {
            get => _gameProgressDisplay;
            set => SetProperty(ref _gameProgressDisplay, value);
        }

        private double _gameProgressValue;
        public double GameProgressValue
        {
            get => _gameProgressValue;
            set => SetProperty(ref _gameProgressValue, value);
        }

        private int _consecutiveWinsCount;
        public int ConsecutiveWinsCount
        {
            get => _consecutiveWinsCount;
            set => SetProperty(ref _consecutiveWinsCount, value);
        }

        private int _maxWinsCount;
        public int MaxWinsCount
        {
            get => _maxWinsCount;
            set => SetProperty(ref _maxWinsCount, value);
        }

        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => SetProperty(ref _isButtonVisible, value);
        }

        private double _stats_WinRatioPercentageValue;
        public double Stats_WinRatioPercentageValue
        {
            get => _stats_WinRatioPercentageValue; 
            set => SetProperty(ref _stats_WinRatioPercentageValue, value);
        }

        private string _stats_WinRatioPercentageDisplay;
        public string Stats_WinRatioPercentageDisplay
        {
            get { return _stats_WinRatioPercentageDisplay; }
            set
            {
                SetProperty(ref _stats_WinRatioPercentageDisplay, value);
            }
        }

        private int _stats_WinCount;
        public int Stats_WinCount
        {
            get { return _stats_WinCount; }
            set
            {
                SetProperty(ref _stats_WinCount, value);
            }
        }

        private int _stats_LostCount;
        public int Stats_LostCount
        {
            get { return _stats_LostCount; }
            set
            {
                SetProperty(ref _stats_LostCount, value);
            }
        }

        private string _heroText;
        public string HeroText
        {
            get { return _heroText; }
            set
            {
                SetProperty(ref _heroText, value);
            }
        }



        public DelegateCommand PlayGameCommand { get; set; }
        public DelegateCommand AboutPageCommand { get; set; }
        public DelegateCommand RulesPageCommand { get; set; }

        public StartPageViewModel() : base()
        {
            this.Title = Constants.ApplicationName;

            PlayGameCommand = new DelegateCommand(PlayGame);
            AboutPageCommand = new DelegateCommand(ShowAboutPage);
            RulesPageCommand = new DelegateCommand(ShowRulesPage);
        }

        public override async void OnNavigatedTo(object parameters)
        {
            await PrepareData();
        }

        private async Task PrepareData()
        {
            try
            {
                //If levels list or valid words list is not loaded, load them
                var levelsCount = await _appDBService.GetAllLevelsCount();
                var wordsCount = await _appDBService.GetAllLevelsCount();

                if (levelsCount == 0 || wordsCount == 0)
                {
                    this.LoadingText = $"Please hold on while we initialize the game for you...";
                    SetDataLodingIndicators(true);

                    await WordlyHelpers.PrepareWordsAndLevels(_appDBService, _appApiService);
                }

                //Get the Levels
                var _allLevels = await _appDBService.GetAllLevels();

                //Get Progress
                var _totalCompleted = _allLevels.Where(_level => (_level.Status == "Won" || _level.Status == "Lost")).Count();
                GameProgressDisplay = $"{_totalCompleted}";
                GameProgressValue = Convert.ToDouble(_totalCompleted) / Convert.ToDouble(_allLevels.Count);
                MaxWinsCount = _appSettingsService.MaxWinsCount;
                ConsecutiveWinsCount = _appSettingsService.ConsecutiveWinsCount;
                Stats_WinRatioPercentageValue = (_totalCompleted == 0? 0d : (Convert.ToDouble(_allLevels.Where(_level => (_level.Status == "Won")).Count()) / Convert.ToDouble(_totalCompleted)));
                Stats_WinRatioPercentageDisplay = (Stats_WinRatioPercentageValue * 100.0d).ToString("0");
                Stats_WinCount = _allLevels.Where(_level => _level.Status == "Won").Count();
                Stats_LostCount = _allLevels.Where(_level => _level.Status == "Lost").Count();

                IsButtonVisible = true;

                if (_allLevels.Where(_level => (_level.Status == "Won" || _level.Status == "Lost")).Count() == _allLevels.Count)
                {
                    //You are done. All levels are completed. Disable the button
                    IsButtonVisible = false;
                }

                if (_totalCompleted == 0)
                    HeroText = "Enrich your vocabulary in a fun way. Challenge yourself and guess the word in just 6 tries. Show off your progress to family and friends, and help them improve their vocabulary as well.";
                else
                {
                    HeroText = $"You have played {_totalCompleted} (out of 2700) games so far. Your win ratio is {Stats_WinRatioPercentageDisplay}%. Check the side menu for more detailed stats and share with family and friends.";
                }

                this.DataLoaded = true;
            }
            catch (InternetConnectionException iex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Slow or no internet connection." + Environment.NewLine + "Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";
            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Something went wrong. If the problem persists, plz contact support at Vocably@xgeno.com with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
                ErrorImage = "error.png";
            }
            finally
            {
                SetDataLodingIndicators(false);
            }
        }

        private async Task PlayGame()
        {
            if (!_appSettingsService.RulesViewed)
            {
                var rulesPage = new RulesPage();
                rulesPage.Disappearing += async (sender2, e2) =>
                {
                    await GoToNewGamePage();
                };

                await NavigationService.PushModalAsync(rulesPage);
            }
            else
            {
                await GoToNewGamePage();
            }
        }

        private async Task GoToNewGamePage()
        {
            var _allLevels = await _appDBService.GetAllLevels();

            if (_allLevels.Where(_level => (_level.Status == "Won" || _level.Status == "Lost")).Count() < _allLevels.Count)
            {
                await NavigationService.PushAsync(new NewGamePage());
            }
        }

        private async Task ShowAboutPage()
        {
            await NavigationService.PushModalAsync(new AboutUsPage());        
        }

        private async Task ShowRulesPage()
        {
            await NavigationService.PushModalAsync(new RulesPage());
        }
              
    }
}
