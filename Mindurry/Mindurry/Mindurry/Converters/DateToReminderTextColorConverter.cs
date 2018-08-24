using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    public class DateToReminderTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var reminderDate = (DateTimeOffset)value;

            if (reminderDate != null)
            {
                if (reminderDate.Date >= DateTimeOffset.Now.Date)
                { return Color.Black; }
                else
                { return Color.White; }
            }
            else
            {
                return Color.Black;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}