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
                var str = (DataModels.Statut)value;
                switch (str)
                {
                    case DataModels.Statut.Libre:
                        return Color.FromRgb(126,211,33);
                    case DataModels.Statut.Reserve:
                        return Color.FromRgb(245,166,35);
                    case DataModels.Statut.Vendu:
                        return Color.FromRgb(159,24,24);
                    default:
                        return Color.Black;
                }
            }
            catch (Exception ex)
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
