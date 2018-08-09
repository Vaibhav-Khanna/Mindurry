using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    class NegateLeadRepeaterTemplateBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Kind = value as string;
            if ((Kind != "mailReceived") && (Kind != "mailOpened"))
            {
                return true;
            }

            else
            {
                return false;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
