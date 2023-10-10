namespace XGENO.Vocably.ViewModels;

public partial class AboutUsPageViewModel : AppViewModelBase
{
    public AboutUsPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = "CONTACT US";
    }

    public override async void OnNavigatedTo(object parameters)
    {
    }

    [RelayCommand]
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
