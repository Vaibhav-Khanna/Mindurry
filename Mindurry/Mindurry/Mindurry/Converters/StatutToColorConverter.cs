using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    public class StatutToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var str = (DataModels.Status)value;
                switch (str)
                {
                    case DataModels.Status.Libre:
                        return Color.FromRgb(126,211,33);
                    case DataModels.Status.Reserve:
                        return Color.FromRgb(245,166,35);
                    case DataModels.Status.Vendu:
                        return Color.FromRgb(159,24,24);
                    case DataModels.Status.Option:
                        return Color.FromRgb(255, 101, 101);
                    default:
                        return Color.Black;
                }
            }
            catch (Exception)
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
