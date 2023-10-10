namespace XGENO.Mobile.Framework.Extensions;

public static class StringExtensions
{
    public static string CleanCacheKey(this string uri) => Regex.Replace((new Regex("[\\~#%&*{}/:<>?|\"-]")).Replace(uri, " "), @"\s+", "_");


    public static string ToTimeAgo(this string baseTime) => Convert.ToDateTime(baseTime).ToTimeAgo();

    public static string ToAge(this string baseTime)
    {
        var _timeSpan = DateTime.Now - Convert.ToDateTime(baseTime);

        return _timeSpan.TotalDays < 365
            ? Convert.ToInt32(_timeSpan.TotalDays).ToString() + " DAYS"
            : Convert.ToDouble(_timeSpan.TotalDays / 365).ToString("#") + " YRS";
    }

    public static bool IsSafeWord(this string title, bool checkForAndroid = false)
    {
        var nsfwWords = new List<string>() {
                            "anus",
                            "ass ",
                            " ass",
                            "asshole",
                            "bitch",
                            "blow job",
                            "c0ck",
                            "c0cks",
                            "c0k",
                            "clit",
                            "cockhead",
                            "cock-head",
                            "cocksucker",
                            "cock-sucker",
                            "cunt",
                            "dick",
                            "dildo",
                            "dyke",
                            "f u c k",
                            "f*ck",
                            "f**k",
                            "f u c k e r",
                            "fuck",
                            "fucker",
                            "fuckin",
                            "****in",
                            "fucks",
                            "jerk-off",
                            "jizz",
                            "lesbian",
                            "orgasm",
                            "penis",
                            "pussy",
                            "queer",
                            "sex",
                            "slut",
                            "vagina",
                            "vulva",
                            "whore",
                            "xrated",
                            "xxx",
                            "bitch",
                            "blowjob",
                            "porn",
                            "nsfw"
                            };

        if (DeviceInfo.Platform == DevicePlatform.Android && !checkForAndroid)
            return true;

        foreach (var _word in nsfwWords)
        {
            if (title.ToLower().Contains(_word))
                return false;
        }

        return true;
    }

    public static TimeSpan ToTimeSpan(this string timeString)
    {
        var dt = DateTime.ParseExact(timeString, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
        return dt.TimeOfDay;
    }


}