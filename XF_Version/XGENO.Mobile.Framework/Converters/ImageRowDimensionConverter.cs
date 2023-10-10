using System;
using System.Globalization;
using Xamarin.Forms;

namespace XGENO.Mobile.Framework.Converters
{
    public class RowDimensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength(System.Convert.ToInt32((double)value / System.Convert.ToDouble(parameter ?? 1.5d)), GridUnitType.Absolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength(System.Convert.ToInt32((double)value * System.Convert.ToDouble(parameter ?? 1.5d)), GridUnitType.Absolute);
        }
    }
}
