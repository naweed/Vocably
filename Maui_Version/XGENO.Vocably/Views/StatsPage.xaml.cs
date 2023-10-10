namespace XGENO.Vocably.Views;

public partial class StatsPage : ViewBase<StatsPageViewModel>
{
    //private int currentSelectedSegment = 0;

    public StatsPage() : base()
    {
        InitializeComponent();
    }

    private void Share_Clicked(object sender, EventArgs e)
    {
        this.ViewModel.ShareStatsCommand.Execute(null);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

    }

    private async void SummaryButton_Tapped(object sender, EventArgs e)
    {
        SetContainerVisibility("SUMMARY");
    }

    private async void GamesButton_Tapped(object sender, EventArgs e)
    {
        SetContainerVisibility("GAMES");
    }

    private async void SetContainerVisibility(string buttonName)
    {

        switch (buttonName)
        {
            case "SUMMARY":
                frmGames.BackgroundColor = Colors.Transparent;
                frmSummary.BackgroundColor = Color.FromArgb("#992C3539");
                StatsView.Opacity = 0;
                StatsView.IsVisible = true;

                await GamesView.FadeTo(0, Constants.ExtraSmallDuration);
                await StatsView.FadeTo(1, Constants.ExtraSmallDuration);

                GamesView.IsVisible = false;
                break;
            case "GAMES":
                frmSummary.BackgroundColor = Colors.Transparent;
                frmGames.BackgroundColor = Color.FromArgb("#992C3539");
                GamesView.Opacity = 0;
                GamesView.IsVisible = true;

                await StatsView.FadeTo(0, Constants.ExtraSmallDuration);
                await GamesView.FadeTo(1, Constants.ExtraSmallDuration);

                StatsView.IsVisible = false;
                break;
        }
    }
}
