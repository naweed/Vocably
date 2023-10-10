namespace XGENO.Vocably.ViewModels;

public partial class AppViewModelBase : ViewModelBase
{
    public INavigation NavigationService { get; set; }
    public Page PageService { get; set; }
    protected IApiService _appApiService { get; set; }
    protected ISettingsService _appSettingsService { get; set; }
    protected IDatabaseService _appDBService { get; set; }

    public AppViewModelBase(IApiService appApiService, ISettingsService appSettingsService, IDatabaseService appDBService)
        : base()
    {
        _appApiService = appApiService;
        _appSettingsService = appSettingsService;
        _appDBService = appDBService;        
    }

    [RelayCommand]
    private async Task NavigateBack() => await NavigationService.PopAsync();

    [RelayCommand]
    private async Task CloseModal() => await NavigationService.PopModalAsync();
}
