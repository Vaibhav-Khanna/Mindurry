﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Converters
{
    class DoubleToBoolConverter : IValueConverter
    {
        /// <summary>Returns false if string is null or empty
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (double)value;
            if (s != 0) { return true; }
            else return false; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

