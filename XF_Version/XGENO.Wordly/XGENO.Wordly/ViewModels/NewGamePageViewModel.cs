using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class NewGamePageViewModel : AppViewModelBase
    {
        private Level _theGame;
        public Level TheGame
        {
            get => _theGame;
            set => SetProperty(ref _theGame, value);
        }

        public event EventHandler<WordEventArgs> WordLoadedEvent;

        public NewGamePageViewModel() : base()
        {
            this.Title = Constants.ApplicationName;
        }

        public override async void OnNavigatedTo(object parameters)
        {
            await PrepareGame();
        }

        public async Task PrepareGame()
        {
            //Get the next game to play
            var _allLevels = await _appDBService.GetAllLevels();
            var playableGamesCount = _allLevels.Where(_level => (_level.Status != "Won" && _level.Status != "Lost")).Count();

            if (playableGamesCount == 0)
            {
                //No game to play
                await PageService.DisplayAlert("GAME OVER", "Congratulations! You are all done. There are no more levels to play.", "Got It!");
                return;
            }

            TheGame = _allLevels.Where(_level => (_level.Status == "Not Started" || _level.Status == "Started")).OrderBy(_order => _order.Level_No).First();

            this.Title = $"LEVEL {TheGame.Level_No}";

            List<Level_Progress> levelProgress = new List<Level_Progress>();

            if (TheGame.Status == "Started")
                levelProgress = await _appDBService.GetLevelProgress(TheGame.Level_No);

            //Start the game
            if (TheGame.Status == "Not Started")
            {
                TheGame.Status = "Started";
                await _appDBService.SaveLevel(TheGame);
            }

            WordLoadedEvent?.Invoke(this, new WordEventArgs() { Word = TheGame.Word, Meaning = TheGame.Meaning, Progress = levelProgress });

            //Set Pre-Game Settings
            _appSettingsService.SetPreGameSettings();
        }

        public async Task<bool> IsValidWord(string word)
        {
            var isValid = await _appDBService.IsValidEnglishWord(word);

            return isValid;
        }

        public async Task SetUserGameResult(bool result)
        {
            //Save game status
            //Set current game status
            TheGame.Status = result? "Won" : "Lost";
            await _appDBService.SaveLevel(TheGame);

            //Set nexzt game status
            var _allLevels = await _appDBService.GetAllLevels();
            var nextGame = _allLevels.Where(_level => (_level.Status == "Locked")).OrderBy(_order => _order.Level_No).First();
            nextGame.Status = "Not Started";
            await _appDBService.SaveLevel(nextGame);


            //Set Wins in Settings
            _appSettingsService.SetPostGameSettings(result);

            await Task.CompletedTask;
        }

        public async Task SaveLevelProgress(int rowNumber, string guessWord, string resultPhrase, bool isCorrect)
        {
            var progress = new Level_Progress();
            progress.Guess_Word = guessWord;
            progress.Is_Correct = isCorrect;
            progress.Level_No = TheGame.Level_No;
            progress.Row_No = rowNumber;
            progress.ResultPhrase = resultPhrase;

            await _appDBService.SaveLeveProgress(progress);

            TheGame.Success_Row++;

            await _appDBService.SaveLevel(TheGame);
        }

        public async Task<bool> Can_Show_Hint()
        {
            if (TheGame.Hint_Used)
                return true;

            var availableHintsCount = _appSettingsService.HintWinsCount / 5;

            if (availableHintsCount <= 0)
            {
                await PageService.DisplayAlert("HINTS", $"You do not have any hint credit. You have to win another {5 - _appSettingsService.HintWinsCount} games to earn a credit.", "Got it!");
                return false;
            }

            var userResult = await PageService.DisplayAlert("USE HINT?", $"Are you sure you want to use a hint credit? You currently have {availableHintsCount} credit{(availableHintsCount>1? "s" : "")}.", "Yes", "No");

            if (!userResult)
                return false;

            _appSettingsService.HintWinsCount -= 5;
            TheGame.Hint_Used = true;
            await _appDBService.SaveLevel(TheGame);

            return true;
        }

        public async Task<List<Level_Progress>> ProgressLevels()
        {
            var levelProgress = await _appDBService.GetLevelProgress(TheGame.Level_No);

            return levelProgress;
        }


    }
}
