namespace XGENO.Vocably.ViewModels;

public partial class LeaderboardPageViewModel : AppViewModelBase
{
    public LeaderboardPageViewModel(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base(appApiService, appSettingsService, appDBService)
    {
        this.Title = "LEADERBOARD";
    }

    public override async void OnNavigatedTo(object parameters)
    {
    }
}
