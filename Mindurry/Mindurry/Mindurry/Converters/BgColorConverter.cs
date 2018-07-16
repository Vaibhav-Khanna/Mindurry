using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
   public class BgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var index = (int)value;

                if (index % 2 == 0)
                return Color.FromHex("F5F4F3");
            else                   
                return Color.White;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}