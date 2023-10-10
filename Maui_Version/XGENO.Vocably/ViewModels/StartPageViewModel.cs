namespace XGENO.Vocably.ViewModels;

public partial class StartPageViewModel : AppViewModelBase
{
    [ObservableProperty]
    private string gameProgressDisplay;

    [ObservableProperty]
    private double gameProgressValue;

    [ObservableProperty]
    private int consecutiveWinsCount;

    [ObservableProperty]
    private int maxWinsCount;

    [ObservableProperty]
    private bool isButtonVisible;

    [ObservableProperty]
    private double statsWinRatioPercentageValue;

    [ObservableProperty]
    private string statsWinRatioPercentageDisplay;

    [ObservableProperty]
    private int statsWinCount;

    [ObservableProperty]
    private int statsLostCount;

    [ObservableProperty]
    private string heroText;


    public StartPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = Constants.ApplicationName;
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
            StatsWinRatioPercentageValue = (_totalCompleted == 0 ? 0d : (Convert.ToDouble(_allLevels.Where(_level => (_level.Status == "Won")).Count()) / Convert.ToDouble(_totalCompleted)));
            StatsWinRatioPercentageDisplay = (StatsWinRatioPercentageValue * 100.0d).ToString("0");
            StatsWinCount = _allLevels.Where(_level => _level.Status == "Won").Count();
            StatsLostCount = _allLevels.Where(_level => _level.Status == "Lost").Count();

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
                HeroText = $"You have played {_totalCompleted} (out of 2700) games so far. Your win ratio is {StatsWinRatioPercentageDisplay}%. Check the side menu for more detailed stats and share with family and friends.";
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

    [RelayCommand]
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
}
