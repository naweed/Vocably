namespace XGENO.Vocably.ViewModels;

public partial class PrivacyPolicyPageViewModel : AppViewModelBase
{
    public PrivacyPolicyPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = "PRIVACY POLICY";
    }

    public override async void OnNavigatedTo(object parameters)
    {
    }
}
