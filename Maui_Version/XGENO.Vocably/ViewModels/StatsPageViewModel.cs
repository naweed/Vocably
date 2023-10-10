namespace XGENO.Vocably.ViewModels;

public partial class StatsPageViewModel : AppViewModelBase
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
    private double statsWinRatioPercentageValue;

    [ObservableProperty]
    private string statsWinRatioPercentageDisplay;

    [ObservableProperty]
    private int statsWinCount;

    [ObservableProperty]
    private int statsLostCount;

    [ObservableProperty]
    private List<Level> gameLevels;

    [ObservableProperty]
    private List<Attempt_Data> guessDistributionData;


    public StatsPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = "MY STATS";
    }

    public override async void OnNavigatedTo(object parameters)
    {
        await PrepareData();
    }

    private async Task PrepareData()
    {
        try
        {
            this.DataLoaded = false;

            //Get the Levels
            var _allLevels = await _appDBService.GetAllLevels();
            var _playedLevels = _allLevels.Where(_level => (_level.Status == "Won" || _level.Status == "Lost")).OrderBy(level => level.Level_No).ToList();
            var _allWonLevels = _playedLevels.Where(_level => _level.Status == "Won");
            var _allLostLevels = _playedLevels.Where(_level => _level.Status == "Lost");
            var _allLevelsCount = _allLevels.Count;
            var _totalCompletedCount = _playedLevels.Count();

            //Get Progress
            GameProgressDisplay = $"{_totalCompletedCount}";
            GameProgressValue = Convert.ToDouble(_totalCompletedCount) * 100d / Convert.ToDouble(_allLevelsCount);

            MaxWinsCount = _appSettingsService.MaxWinsCount;
            ConsecutiveWinsCount = _appSettingsService.ConsecutiveWinsCount;

            StatsWinRatioPercentageValue = (_totalCompletedCount == 0 ? 0d : (Convert.ToDouble(_allWonLevels.Count()) * 100d / Convert.ToDouble(_totalCompletedCount)));
            StatsWinRatioPercentageDisplay = StatsWinRatioPercentageValue.ToString("0");

            StatsWinCount = _allWonLevels.Count();
            StatsLostCount = _allLostLevels.Count();


            //Get all Level Progress
            var _allLevelProgress = await _appDBService.GetAllLevelProgress();

            //Set Display Characters
            //TODO: Get from Resources: notPresentWordcolor = (Color) App.Current.Resources["NotPresentWordcolor"];
            var notPresentWordcolor = Color.FromArgb("#808080");
            var correctWordColor = Color.FromArgb("#228B22");
            var wrongPlaceColor = Color.FromArgb("#f7d560");
            var emptyBoxColor = Color.FromArgb("#333333");


            _allLevelProgress.ForEach(_prog => { _prog.Display_Chars = WordlyHelpers.BreakWordleLetters(_prog.Guess_Word, _prog.ResultPhrase, correctWordColor, notPresentWordcolor, wrongPlaceColor, emptyBoxColor); });

            //Set Level Details for each level
            _playedLevels.ForEach(level => { level.Level_Details = _allLevelProgress.Where(_prog => _prog.Level_No == level.Level_No).OrderBy(_prog => _prog.Row_No).ToList(); });


            //All Played Levels
            GameLevels = _playedLevels;

            //Attempt Stats
            var attemptStats = new List<Attempt_Data>();
            int maxAttemps = 0;

            for (int i = 1; i <= 6; i++)
            {
                var attemptCount = _allWonLevels.Where(att => (att.Success_Row == i)).Count();

                if (attemptCount > maxAttemps)
                    maxAttemps = attemptCount;

                attemptStats.Add(new Attempt_Data()
                {
                    Attempt_No = i,
                    Attempt_Count = attemptCount
                });
            }

            maxAttemps = (maxAttemps == 0 ? 1 : maxAttemps);

            attemptStats.ForEach(attempt => attempt.Percentage = (Convert.ToSingle(attempt.Attempt_Count) / Convert.ToSingle(maxAttemps)));

            GuessDistributionData = attemptStats;

            this.DataLoaded = true;
        }
        catch (Exception ex)
        {
            this.IsErrorState = true;
            this.ErrorMessage = "Something went wrong. If the problem persists, plz contact support at Vocably@xgeno.com with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
            ErrorImage = "error.png";
        }
        finally
        {
        }
    }

    [RelayCommand]
    private async Task ShareStats()
    {
        var textToShare = $"My #Vocably Stats" + Environment.NewLine;

        //Add Total Games
        textToShare += $"Played: {GameProgressDisplay} (of 2700){Environment.NewLine}";

        //Add Win Ratio
        textToShare += $"Win Ratio: {(StatsWinRatioPercentageValue * 100.0d).ToString("0.0")}%{Environment.NewLine}";

        //Add Guess Disribution
        textToShare += $"Guess Distribution:{Environment.NewLine}";

        var totalGuesses = GuessDistributionData.Sum(g => g.Attempt_Count);

        //Rank the guesses
        var ranks = GuessDistributionData.OrderBy(n => n.Attempt_Count).Select((n, i) => new GuessRank() { Attempt_No = n.Attempt_No, Rank = i + 1, Attempt_Count = n.Attempt_Count }).ToList();

        foreach (var guess in GuessDistributionData)
        {
            textToShare += $"{guess.Attempt_No} | ";

            if (guess.Attempt_Count == 0)
            {
                textToShare += "-";
            }
            else
            {
                for (int i = 0; i < ranks.Where(r => r.Attempt_Count == guess.Attempt_Count).OrderByDescending(o => o.Rank).First().Rank; i++)
                {
                    textToShare += "■";
                }

                textToShare += $" ({guess.Attempt_Count})";
            }


            textToShare += Environment.NewLine;
        }

        //Add Game URL
        textToShare += $"{Environment.NewLine}https://vocably-game.com";

        //Share
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = textToShare,
            Title = "VOCABLY"
        });
    }


}
