using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Gallery.ViewModel.Helpers
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).Year;
        }




        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
