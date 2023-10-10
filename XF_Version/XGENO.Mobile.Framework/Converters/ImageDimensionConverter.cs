using System;
using System.Globalization;
using Xamarin.Forms;

namespace XGENO.Mobile.Framework.Converters
{
    public class ImageDimensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * System.Convert.ToDouble(parameter?? 1.5d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / System.Convert.ToDouble(parameter ?? 1.5d);
        }
    }
}
