using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using XGENO.Vocably.Platforms.Dependencies;
using IDeviceInfo = XGENO.Mobile.Framework.UI.IDeviceInfo;

namespace XGENO.Vocably;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureSyncfusionCore()
			.UseMauiCommunityToolkit()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("BarlowCondensed-Medium.ttf", "MediumCondensedFont");
				fonts.AddFont("FiraSansCondensed-Regular.ttf", "ThinFont");
				fonts.AddFont("FiraSans-Light.ttf", "LightFont");
				fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
				fonts.AddFont("FiraSans-Regular.ttf", "RegularFont");
				fonts.AddFont("FiraSans-SemiBold.ttf", "BoldFont");
			})
			.ConfigureLifecycleEvents(events =>
			{
#if ANDROID
				events.AddAndroid(android => android
					.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

				static void MakeStatusBarTranslucent(Android.App.Activity activity)
				{
					activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

					activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

					activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
				}
#endif
			});

		//Register Cache Barrel
		Barrel.ApplicationId = Constants.ApplicationId;

		//App API Service to DI Container
		var appApiService = new WordsApiService(Constants.ApiServiceURL);
		appApiService.SetCacheBarrel(Barrel.Current);
		builder.Services.AddSingleton<IApiService>(appApiService); //API Service

		builder.Services.AddSingleton<ISettingsService>(new AppSettingsService()); //Settings Service
		builder.Services.AddSingleton<IDatabaseService>(new AppDBService()); //Database Service

		//Add Platform specific Dependencies
		builder.Services.AddTransient<IDeviceInfo, DeviceInfoService>();

		//Add View Models to DI Container
		builder.Services.AddTransient<StartPageViewModel>();
		builder.Services.AddTransient<StatsPageViewModel>();
		builder.Services.AddTransient<AboutUsPageViewModel>();
		builder.Services.AddTransient<PrivacyPolicyPageViewModel>();
		builder.Services.AddTransient<NewGamePageViewModel>();
		builder.Services.AddTransient<RulesPageViewModel>();
		builder.Services.AddTransient<LeaderboardPageViewModel>();

		return builder.Build();
	}
}

