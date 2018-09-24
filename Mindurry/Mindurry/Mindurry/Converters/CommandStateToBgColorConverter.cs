using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    class CommandStateToBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var commandState = (string)value;

            if (commandState != null)
            {
                if (commandState == CommandState.Libre.ToString())
                {
                    return Color.White;
                }
                if (commandState == CommandState.Reservé.ToString())
                {
                    //return Color.FromHex("4A90E2");
                    return Color.FromHex("DDF2FF");
                }
                if (commandState == CommandState.Optionné.ToString())
                {
                    return Color.FromHex("F8E4C3");
                }
                if (commandState == CommandState.Acté.ToString())
                {
                    return Color.FromHex("E4FCCD");
                }
                else 
                {
                    return Color.FromHex("FAD1D1");
                }
            }
            else
            {
                return Color.FromHex("FAD1D1");
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}