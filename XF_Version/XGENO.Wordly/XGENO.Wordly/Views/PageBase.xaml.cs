using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XGENO.Mobile.Framework.UI;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.Views
{
    public partial class PageBase : ContentPage
    {
        public IList<View> PageContent => PageContentGrid.Children;
        public IList<View> PageIcons => PageIconsGrid.Children;

        protected bool IsBackButtonEnabled
        {
            set => NavigateBackButton.IsEnabled = value;
        }

        protected bool IsPhoneX
        {
            get
            {
                var deviceInfo = Xamarin.Essentials.DeviceInfo.Model;

                return (Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.iOS && (deviceInfo == "iPhone10,3" || deviceInfo == "iPhone10,6" || deviceInfo.StartsWith("iPhone11,") || deviceInfo.StartsWith("iPhone12,") || deviceInfo.StartsWith("iPhone13,") || deviceInfo.StartsWith("iPhone14,")));
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
            StatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;
            MenuStatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;

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

        private async void HamburgerButton_Clicked(System.Object sender, System.EventArgs e)
        {
            _ = MainContentGrid.TranslateTo(this.Width * 3 / 5 - (Device.RuntimePlatform == Device.iOS ? 24 : 24), this.Height * 0.1, Constants.SmallDuration, Easing.CubicIn);
            await MainContentGrid.ScaleTo(0.8, Constants.SmallDuration);
            _ = MainContentGrid.FadeTo(0.8, Constants.SmallDuration);
            await MenuCloseButton.FadeTo(1, Constants.LongDuration);
            MainContentGrid.IsEnabled = false;

        }

        private async void CloseMenuButton_Clicked(System.Object sender, System.EventArgs e)
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
        }

        private async void ContactUs_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutUsPage());
            CloseMenuButton_Clicked(null, null);
        }

        private async void PrivacyPolicy_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PrivacyPolicyPage());
            CloseMenuButton_Clicked(null, null);
        }

        private async void GameRules_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new RulesPage());
            CloseMenuButton_Clicked(null, null);
        }

        private async void Statistics_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new StatsPage());
            CloseMenuButton_Clicked(null, null);
        }

        private async void Leaderboard_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new LeaderboardPage());
            CloseMenuButton_Clicked(null, null);
        }



        private async void Wordle_Clicked(System.Object sender, System.EventArgs e)
        {
            var wordleURI = @"https://www.powerlanguage.co.uk/wordle/";
            await Browser.OpenAsync(wordleURI, new BrowserLaunchOptions { LaunchMode = BrowserLaunchMode.External, TitleMode = BrowserTitleMode.Hide });

            CloseMenuButton_Clicked(null, null);
        }

        private async void GridArea_Tapped(System.Object sender, System.EventArgs e)
        {
            await CloseMenu();
        }
    }
}
