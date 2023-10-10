namespace XGENO.Vocably.ViewModels;

public partial class RulesPageViewModel : AppViewModelBase
{
    public RulesPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = "GAME RULES";
    }

    public override async void OnNavigatedTo(object parameters)
    {
        //Marks rules as viewed
        if (!_appSettingsService.RulesViewed)
            _appSettingsService.RulesViewed = true;
    }
}