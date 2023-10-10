using System;

namespace XGENO.Wordly.Interfaces
{
    public interface ISettingsService
    {
        DateTime GameStartDate { get; set; }
        DateTime LastGameLostDate { get; set; }
        int HintWinsCount { get; set; }
        int ConsecutiveWinsCount { get; set; }
        int MaxWinsCount { get; set; }
        bool RulesViewed { get; set; }
        bool PronunciationEnabled { get; set; }

        void SetPreGameSettings();
        void SetPostGameSettings(bool didWin);
        void UseHint();
    }
}
