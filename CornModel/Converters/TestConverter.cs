using System;
using System.Globalization;
using CornModel.Abstract;
using System.Windows;

namespace CornModel.Converters
{
    public class TestConverter : ConverterBase<TestConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          var rez = 60;
          if ((bool)value) rez = 200; 
          return rez;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BoolToVisibleConverter : ConverterBase<BoolToVisibleConverter>
    {
      
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;
            if (parameter != null && parameter.Equals("Hidden")) visibility = Visibility.Hidden;
            return (bool)value ? Visibility.Visible : visibility;
        }
    }
}