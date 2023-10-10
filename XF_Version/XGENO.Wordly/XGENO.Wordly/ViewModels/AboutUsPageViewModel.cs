using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.MVVM;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.ViewModels
{
    public class AboutUsPageViewModel : AppViewModelBase
    {
        public DelegateCommand SendFeedbackCommand { get; set; }


        public AboutUsPageViewModel() : base()
        {
            this.Title = "CONTACT US";

            SendFeedbackCommand = new DelegateCommand(SendFeedback);
        }

        public override async void OnNavigatedTo(object parameters)
        {
        }

        private async Task SendFeedback()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = $"{Constants.ApplicationName} Feedback",
                    To = new List<string>() { $"{Constants.ApplicationName}@xgenoapps.com" }
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
    }
}
