namespace XGENO.Vocably;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//Enable Version Tracking
		VersionTracking.Track();

		MainPage = new NavigationPage(new StartPage());
	}
}

