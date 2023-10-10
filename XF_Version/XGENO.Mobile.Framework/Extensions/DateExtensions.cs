using System;

namespace XGENO.Mobile.Framework.Extensions
{
    public static partial class DateExtensions
    {
        public static string ToUtcJSONDateTimeString(this DateTime theDate)
        {
            return theDate.ToUniversalTime().ToString("yyyy-MM-dd") + "T" + theDate.ToUniversalTime().ToString("hh:mm:ss") + ".000Z";
        }

        public static string ToTimeAgo(this DateTime baseTime)
        {
            var _timeSpan = DateTime.Now - baseTime;

            if (_timeSpan.TotalMinutes == 0)
                return "Just now";

            if (_timeSpan.TotalMinutes < 60)
                return Convert.ToInt32(_timeSpan.TotalMinutes).ToString() + " mins ago";

            if (_timeSpan.TotalHours < 2)
                return Convert.ToInt32(_timeSpan.TotalHours).ToString() + " hour ago";

            if (_timeSpan.TotalHours < 24)
                return Convert.ToInt32(_timeSpan.TotalHours).ToString() + " hours ago";

            if (_timeSpan.TotalDays < 2)
                return Convert.ToInt32(_timeSpan.TotalDays).ToString() + " day ago";

            if (_timeSpan.TotalDays < 365)
                return Convert.ToInt32(_timeSpan.TotalDays).ToString() + " days ago";

            if (Convert.ToDouble(_timeSpan.TotalDays / 365) < 2)
                return "1 year ago";

            return Convert.ToDouble(_timeSpan.TotalDays / 365).ToString("#") + " years ago";
        }

        public static string ToString(this DateTime dateTime, string format, bool useExtendedSpecifiers)
        {
            return useExtendedSpecifiers
                ? dateTime.ToString(format)
                    .Replace("nn", dateTime.Day.ToOccurrenceSuffix().ToLower())
                    .Replace("NN", dateTime.Day.ToOccurrenceSuffix().ToUpper())
                : dateTime.ToString(format);
        }

        private static string ToOccurrenceSuffix(this int integer)
        {
            switch (integer % 100)
            {
                case 11:
                case 12:
                case 13:
                    return "th";
            }
            switch (integer % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

    }
}
