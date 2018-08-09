using System;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    public class LeadRepeaterTemplateBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Kind = value as string;
            if ((Kind != "mailReceived") && (Kind != "mailOpened"))
            {
                return false;
            }

            else
            {
                return true;
            }
                

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
