using System;
using System.Globalization;
using Xamarin.Forms;

namespace XGENO.Mobile.Framework.Converters
{
    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = System.Convert.ToString(parameter) ?? "u";

            return (param.ToUpper()) switch
            {
                "U" => ((string)value).ToUpper(),
                "L" => ((string)value).ToLower(),
                _ => ((string)value),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
