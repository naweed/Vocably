using System;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Interfaces;

namespace XGENO.Wordly.Services
{
    public class AppSettingsService : BindableBase, ISettingsService
    {
        private DateTime _gameStartDate;
        public DateTime GameStartDate
        {
            get
            {
                _gameStartDate = Preferences.Get(nameof(GameStartDate), new DateTime(2000, 1, 1));
                return _gameStartDate;
            }
            set
            {
                SetProperty(ref _gameStartDate, value);
                Preferences.Set(nameof(GameStartDate), _gameStartDate);
            }
        }

        private DateTime _lastGameLostDate;
        public DateTime LastGameLostDate
        {
            get
            {
                _lastGameLostDate = Preferences.Get(nameof(LastGameLostDate), new DateTime(2000, 1, 1));
                return _lastGameLostDate;
            }
            set
            {
                SetProperty(ref _lastGameLostDate, value);
                Preferences.Set(nameof(LastGameLostDate), _lastGameLostDate);
            }
        }


        private int _hintWinsCount;
        public int HintWinsCount
        {
            get
            {
                _hintWinsCount = Preferences.Get(nameof(HintWinsCount), -1);
                return _hintWinsCount;
            }
            set
            {
                SetProperty(ref _hintWinsCount, value);
                Preferences.Set(nameof(HintWinsCount), _hintWinsCount);
            }
        }

        private int _consecutiveWinsCount;
        public int ConsecutiveWinsCount
        {
            get
            {
                _consecutiveWinsCount = Preferences.Get(nameof(ConsecutiveWinsCount), 0);
                return _consecutiveWinsCount;
            }
            set
            {
                SetProperty(ref _consecutiveWinsCount, value);
                Preferences.Set(nameof(ConsecutiveWinsCount), _consecutiveWinsCount);
            }
        }

        private int _maxWinsCount;
        public int MaxWinsCount
        {
            get
            {
                _maxWinsCount = Preferences.Get(nameof(MaxWinsCount), 0);
                return _maxWinsCount;
            }
            set
            {
                SetProperty(ref _maxWinsCount, value);
                Preferences.Set(nameof(MaxWinsCount), _maxWinsCount);
            }
        }

        private bool _rulesViewed;
        public bool RulesViewed
        {
            get
            {
                _rulesViewed = Preferences.Get(nameof(RulesViewed), false);
                return _rulesViewed;
            }
            set
            {
                SetProperty(ref _rulesViewed, value);
                Preferences.Set(nameof(RulesViewed), _rulesViewed);
            }
        }

        private bool _pronunciationEnabled;
        public bool PronunciationEnabled
        {
            get
            {
                _pronunciationEnabled = Preferences.Get(nameof(PronunciationEnabled), true);
                return _pronunciationEnabled;
            }
            set
            {
                SetProperty(ref _pronunciationEnabled, value);
                Preferences.Set(nameof(PronunciationEnabled), _pronunciationEnabled);
            }
        }



        public AppSettingsService()
        {
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            //Set the Wins Count to 30 (6 hints)
            if (HintWinsCount == -1)
                HintWinsCount = 30;
        }

        public void SetPreGameSettings()
        {
            //Set First Game Start Date
            if (GameStartDate.Year == 2000)
                GameStartDate = DateTime.Now;
        }

        public void SetPostGameSettings(bool didWin)
        {
            switch (didWin)
            {
                case true:
                    ConsecutiveWinsCount++;
                    HintWinsCount++;
                    if (ConsecutiveWinsCount > MaxWinsCount)
                        MaxWinsCount = ConsecutiveWinsCount;
                    break;
                case false:
                    ConsecutiveWinsCount = 0;
                    LastGameLostDate = DateTime.Now;
                    break;
            }

        }

        public void UseHint()
        {
            //Use a Hint
            HintWinsCount -= 10;
        }
    }
}
