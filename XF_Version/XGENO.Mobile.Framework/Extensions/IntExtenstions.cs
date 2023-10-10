using System;

namespace XGENO.Mobile.Framework.Extensions
{
    public static class IntExtenstions
    {
        public static string DisplayTime(this int timeInMins)
        {
            if (timeInMins <= 60)
                return timeInMins.ToString() + " MINS";

            if (timeInMins <= 24 * 60)
                return (timeInMins / 60).ToString() + " HRS, " + (timeInMins % 60).ToString() + " MINS";

            //Return full time
            return (timeInMins / (24 * 60)).ToString() + " DAYS, " + ((timeInMins % (24 * 60)) / 60).ToString() + " HRS, " + ((timeInMins % (24 * 60)) % 60).ToString() + " MINS";
        }
    }
}
