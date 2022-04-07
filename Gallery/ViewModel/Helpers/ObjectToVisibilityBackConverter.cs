using System;
using System.Windows;
using System.Windows.Data;

namespace Gallery.ViewModel.Helpers
{
    public class ObjectToVisibilityBackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = value is null;
            if (!bValue)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Collapsed)
                return true;
            else
                return false;
        }
    }
}
