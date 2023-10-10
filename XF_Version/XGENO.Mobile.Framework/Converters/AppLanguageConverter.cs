using System;
using System.Globalization;
using Xamarin.Forms;

namespace XGENO.Mobile.Framework.Converters
{
    public class AppLanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value?.ToString()) switch
            {
                "AR" => "ARABIC",
                "ZH" => "CHINESE",
                "HR" => "CROATIAN",
                "CS" => "CZECH",
                "DA" => "DANISH",
                "NL" => "DUTCH",
                "EN" => "ENGLISH",
                "FI" => "FINNISH",
                "FR" => "FRENCH",
                "DE" => "GERMAN",
                "EL" => "GREEK",
                "HI" => "HINDI",
                "ID" => "INDONESIAN",
                "IT" => "ITALIAN",
                "JA" => "JAPANESE",
                "KO" => "KOREAN",
                "FA" => "PERSIAN",
                "PL" => "POLISH",
                "PT" => "PORTUGUESE",
                "RU" => "RUSSIAN",
                "ES" => "SPANISH",
                "SV" => "SWEDISH",
                "TR" => "TURKISH",
                "UR" => "URDU",
                "ZU" => "ZULU",
                _ => "ENGLISH",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
