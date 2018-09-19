using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    public class ExposureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var exposition = (string)value;

            switch (exposition)
            {
                case "east":
                    return "Est";
                case "East":
                    return "Est";
                case "nortWest":
                    return "Nord Ouest";
                case "NortWest":
                    return "Nord Ouest";
                case "north":
                    return "Nord";
                case "North":
                    return "Nord";
                case "northEast":
                    return "Nord Est";
                case "NorthEast":
                    return "Nord Est";
                case "south":
                    return "Sud";
                case "South":
                    return "Sud";
                case "southEast":
                    return "Sud Est";
                case "SouthEast":
                    return "Sud Est";
                case "southWest":
                    return "Sud Ouest";
                case "SouthWest":
                    return "Sud Ouest";
                case "west":
                    return "Ouest";
                case "West":
                    return "Ouest";
                default:
                    return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}