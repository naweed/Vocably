using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Helpers;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class StatsPageViewModel : AppViewModelBase
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

        private List<Level> _gamesLevels;
        public List<Level> GameLevels
        {
            get { return _gamesLevels; }
            set
            {
                SetProperty(ref _gamesLevels, value);
            }
        }

        private List<Attempt_Data> _guessDistributionData;
        public List<Attempt_Data> GuessDistributionData
        {
            get => _guessDistributionData;
            set => SetProperty(ref _guessDistributionData, value);
        }

        public DelegateCommand ShareCommand { get; set; }

        public StatsPageViewModel() : base()
        {
            this.Title = "MY STATS";

            ShareCommand = new DelegateCommand(ShareStats);
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
                GameProgressValue = Convert.ToDouble(_totalCompletedCount) / Convert.ToDouble(_allLevelsCount);

                MaxWinsCount = _appSettingsService.MaxWinsCount;
                ConsecutiveWinsCount = _appSettingsService.ConsecutiveWinsCount;

                Stats_WinRatioPercentageValue = (_totalCompletedCount == 0 ? 0d : (Convert.ToDouble(_allWonLevels.Count()) / Convert.ToDouble(_totalCompletedCount)));
                Stats_WinRatioPercentageDisplay = (Stats_WinRatioPercentageValue * 100.0d).ToString("0");

                Stats_WinCount = _allWonLevels.Count();
                Stats_LostCount = _allLostLevels.Count();


                //Get all Level Progress
                var _allLevelProgress = await _appDBService.GetAllLevelProgress();

                //Set Display Characters
                //TODO: Get from Resources: notPresentWordcolor = (Color) App.Current.Resources["NotPresentWordcolor"];
                var notPresentWordcolor = ColorConverters.FromHex("#808080");
                var correctWordColor = ColorConverters.FromHex("#228B22");
                var wrongPlaceColor = ColorConverters.FromHex("#f7d560");
                var emptyBoxColor = ColorConverters.FromHex("#333333");


                _allLevelProgress.ForEach(_prog => { _prog.Display_Chars = WordlyHelpers.BreakWordleLetters(_prog.Guess_Word, _prog.ResultPhrase, correctWordColor, notPresentWordcolor, wrongPlaceColor, emptyBoxColor); });

                //Set Level Details for each level
                _playedLevels.ForEach(level => { level.Level_Details = _allLevelProgress.Where(_prog => _prog.Level_No == level.Level_No).OrderBy(_prog => _prog.Row_No).ToList(); });

                ////Fill in Empty Rows
                //_playedLevels.ForEach(level =>
                //                {
                //                    for (int i = level.Level_Details.Count + 1; i <= 6; i++)
                //                    {
                //                        level.Level_Details.Add(new Level_Progress() { Guess_Word = "     ", ID = 0, Is_Correct = false, Level_No = i, ResultPhrase = "     ", Row_No = i, Display_Chars = WordlyHelpers.BreakWordleLetters("     ", "     ", correctWordColor, notPresentWordcolor, wrongPlaceColor, emptyBoxColor) });
                //                    }
                //                });


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

        private async Task ShareStats()
        {
            var textToShare = $"My #Vocably Stats" + Environment.NewLine;

            //Add Total Games
            textToShare += $"Played: {GameProgressDisplay} (of 2700){Environment.NewLine}";

            //Add Win Ratio
            textToShare += $"Win Ratio: {(Stats_WinRatioPercentageValue * 100.0d).ToString("0.0")}%{Environment.NewLine}";

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
}
