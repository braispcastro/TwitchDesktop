using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TwitchDesktop.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        protected static Visibility ParseOrDefault(string str, Visibility defaultVisibility)
        {
            var result = defaultVisibility;
            if (string.IsNullOrWhiteSpace(str)) return result;
            try
            {
                result = (Visibility)Enum.Parse(typeof(Visibility), str);
            }
            catch (Exception) { }
            return result;
        }
        
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = Visibility.Hidden;
            bool flag = (bool)value;
            
            if (parameter is Visibility)
                visibility = (Visibility)parameter;
            else
                visibility = ParseOrDefault(parameter as string, visibility);

            return flag ? Visibility.Visible : visibility;
        }
        
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible;
        }
    }
}
