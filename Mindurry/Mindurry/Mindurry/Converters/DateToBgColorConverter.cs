using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    public class DateToBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var reminderDate = (DateTime)value;

            if (reminderDate != null)
            {
                if (reminderDate.Date == DateTime.Now.Date)
                    return Color.FromHex("f5a623");
                else if (reminderDate.Date > DateTime.Now.Date)
                    return Color.Red;
                else return Color.White;

            }
            else
            {
                return Color.White;
            }
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}