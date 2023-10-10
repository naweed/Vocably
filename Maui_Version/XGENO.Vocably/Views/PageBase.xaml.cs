using IDeviceInfo = XGENO.Mobile.Framework.UI.IDeviceInfo;

namespace XGENO.Vocably.Views;

public partial class PageBase : ContentPage
{
	public IList<Microsoft.Maui.IView> PageContent => PageContentGrid.Children;
	public IList<Microsoft.Maui.IView> PageIcons => PageIconsGrid.Children;

    protected bool IsBackButtonEnabled
    {
        set => NavigateBackButton.IsEnabled = value;
    }

    protected bool IsPhoneX
    {
        get
        {
            var deviceInfo = DeviceInfo.Model;

            return (DeviceInfo.Platform == DevicePlatform.iOS && (deviceInfo == "iPhone10,3" || deviceInfo == "iPhone10,6" || deviceInfo.StartsWith("iPhone11,") || deviceInfo.StartsWith("iPhone12,") || deviceInfo.StartsWith("iPhone13,") || deviceInfo.StartsWith("iPhone14,")));
        }
    }

    #region Bindable properties
    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(PageBase), string.Empty, defaultBindingMode: BindingMode.OneWay, propertyChanged: OnPageTitleChanged);

    public static readonly BindableProperty PageModeProperty = BindableProperty.Create(nameof(PageMode), typeof(PageMode), typeof(PageBase), PageMode.None, propertyChanged: OnPageModePropertyChanged);

    public static readonly BindableProperty ContentDisplayModeProperty = BindableProperty.Create(nameof(ContentDisplayMode), typeof(ContentDisplayMode), typeof(PageBase), ContentDisplayMode.NoNavigationBar, propertyChanged: OnContentDisplayModePropertyChanged);

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }

    public PageMode PageMode
    {
        get => (PageMode)GetValue(PageModeProperty);
        set => SetValue(PageModeProperty, value);
    }

    public ContentDisplayMode ContentDisplayMode
    {
        get => (ContentDisplayMode)GetValue(ContentDisplayModeProperty);
        set => SetValue(ContentDisplayModeProperty, value);
    }

    private static void OnPageTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
        {
            basePage.TitleLabel.Text = (string)newValue;
            basePage.HeaderTextFrame.IsVisible = true;
        }
    }

    private static void OnPageModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
            basePage.SetPageMode((PageMode)newValue);
    }

    private static void OnContentDisplayModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
            basePage.SetContentDisplayMode((ContentDisplayMode)newValue);
    }
    #endregion

    public PageBase()
	{
		InitializeComponent();

        //Hide the Xamarin Forms build in navigation header
        NavigationPage.SetHasNavigationBar(this, false);

        //Fix Top Margin Height
        StatusRowDefinition.Height = ServiceHelpers.GetService<IDeviceInfo>().StatusbarHeight;
        MenuStatusRowDefinition.Height = ServiceHelpers.GetService<IDeviceInfo>().StatusbarHeight;

        //Set Page Mode
        SetPageMode(PageMode.None);

        //Set Content Display Mode
        SetContentDisplayMode(ContentDisplayMode.NoNavigationBar);

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void SetPageMode(PageMode pageMode)
    {
        switch (pageMode)
        {
            case PageMode.Menu:
                HamburgerButton.IsVisible = true;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Navigate:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = true;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Modal:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = true;
                break;
            default:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
        }
    }

    private void SetContentDisplayMode(ContentDisplayMode contentDisplayMode)
    {
        switch (contentDisplayMode)
        {
            case ContentDisplayMode.NavigationBar:
                Grid.SetRowSpan(PageContentGrid, 1);
                Grid.SetRow(PageContentGrid, 2);
                break;
            case ContentDisplayMode.NoNavigationBar:
                //Set Icon styles
                //NavigateBackButton.Style = (Style)Application.Current.Resources["ImageIconButtonStyle"];
                break;
            default:
                //Do Nothing
                break;
        }
    }

    protected async void GridArea_Tapped(object sender, EventArgs e)
    {
        await CloseMenu();
    }

    private async Task CloseMenu()
    {
        _ = MenuCloseButton.FadeTo(0, Constants.ExtraSmallDuration);
        _ = MainContentGrid.FadeTo(1, Constants.SmallDuration);
        _ = MainContentGrid.ScaleTo(1, Constants.SmallDuration);
        await MainContentGrid.TranslateTo(0, 0, Constants.SmallDuration, Easing.CubicIn);
        Device.BeginInvokeOnMainThread(() => MainContentGrid.IsEnabled = true);
        Device.BeginInvokeOnMainThread(() => MenuContainer.IsEnabled = false);
    }

    protected async void HamburgerButton_Clicked(object sender, EventArgs e)
    {
        Device.BeginInvokeOnMainThread(() => MenuContainer.IsEnabled = true);
        _ = MainContentGrid.TranslateTo(this.Width * 3 / 5 - (DeviceInfo.Platform == DevicePlatform.iOS ? 24 : 24), this.Height * 0.1, Constants.SmallDuration, Easing.CubicIn);
        await MainContentGrid.ScaleTo(0.8, Constants.SmallDuration);
        _ = MainContentGrid.FadeTo(0.8, Constants.SmallDuration);
        await MenuCloseButton.FadeTo(1, Constants.LongDuration);
        Device.BeginInvokeOnMainThread(() => MainContentGrid.IsEnabled = false);
    }

    protected async void CloseMenuButton_Clicked(object sender, EventArgs e) =>
        await CloseMenu();

    protected async void CloseButton_Tapped(object sender, EventArgs e) =>
        await Navigation.PopModalAsync();

    protected async void NavigateBackButton_Tapped(object sender, EventArgs e) =>
        await Navigation.PopAsync();

    protected async void ContactUs_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AboutUsPage());
        CloseMenuButton_Clicked(null, null);
    }

    protected async void PrivacyPolicy_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PrivacyPolicyPage());
        CloseMenuButton_Clicked(null, null);
    }

    protected async void GameRules_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RulesPage());
        CloseMenuButton_Clicked(null, null);
    }

    protected async void Statistics_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new StatsPage());
        CloseMenuButton_Clicked(null, null);
    }

    protected async void Leaderboard_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LeaderboardPage());
        CloseMenuButton_Clicked(null, null);
    }
}