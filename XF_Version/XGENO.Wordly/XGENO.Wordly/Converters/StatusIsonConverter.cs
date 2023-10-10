using System;
using System.Globalization;
using Xamarin.Forms;

namespace XGENO.Wordly.Converters
{
    public class StatusIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((!String.IsNullOrEmpty(value.ToString()) && value.ToString() != "Lost") ? "tickyes.png" : "tickno.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
