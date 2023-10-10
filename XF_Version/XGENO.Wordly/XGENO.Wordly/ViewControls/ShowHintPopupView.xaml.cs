using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.Services;
using XGENO.Wordly.Interfaces;

namespace XGENO.Wordly.ViewControls
{
    public partial class ShowHintPopupView : PopupPage
    {
        private string meaning, word;

        public ShowHintPopupView(string _meaning, string _word)
        {
            InitializeComponent();

            meaning = _meaning;
            word = _word;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var firstChar = word.Substring(0, 1).ToUpper();

            var aAn = "a";

            switch (firstChar)
            {
                case "A":
                case "E":
                case "F":
                case "H":
                case "I":
                case "L":
                case "M":
                case "N":
                case "O":
                case "R":
                case "S":
                case "X":
                    aAn = "an";
                    break;
                default:
                    aAn = "a";
                    break;
            }

            txtTitle.Text = "This word starts with " + aAn + " \"" + firstChar + "\", and it means:";

            txtMeaning.Text = "\"" + meaning + "\"";
        }

        void CloseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        //protected override void OnAppearingAnimationEnd()
        //{
        //    base.OnAppearingAnimationEnd();

        //    var _appSettingsService = ContainerService.GetSingleton<ISettingsService>();

        //    if (_appSettingsService.PronunciationEnabled)
        //    {
        //        var textToSpeak = "This word means, \"" + meaning + "\"";
        //        _ = TextToSpeech.SpeakAsync(textToSpeak);
        //    }
        //}
    }
}
