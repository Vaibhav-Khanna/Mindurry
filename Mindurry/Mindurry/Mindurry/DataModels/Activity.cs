using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.DataModels
{
    public class Activity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }    
        public string Message { get; set; }
        public Color Color { get; set; }
        public Xamarin.Forms.LayoutOptions HorizontalOptions { get; set; }
        public string Icon { get; set; }
    }
}
