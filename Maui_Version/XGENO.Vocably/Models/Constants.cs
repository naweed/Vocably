namespace XGENO.Vocably.Models;

public static class Constants
{
    public static string ApplicationName = "VOCABLY";
    public static string ApplicationId = "XGENO.Wordly.Mobile";
    public static string ApiServiceURL = @"https://xxxxxxxxx.com/api/vocably/v.1.0/";

    public static string WordsList = "Words_List";
    public static string LevelsList = "Levels_List";
    public static string IndividualLevel = "Level_";

    public static uint MicroDuration { get; set; } = 60;
    public static uint ExtraSmallDuration { get; set; } = 100;
    public static uint SmallDuration { get; set; } = 300;
    public static uint MediumDuration { get; set; } = 600;
    public static uint LongDuration { get; set; } = 1200;
    public static uint ExtraLongDuration { get; set; } = 1800;
}