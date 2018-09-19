using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
    public partial class LeadDetailPage : ContentPage
    {
        public LeadDetailPage()
        {
            InitializeComponent();
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var toto = sender;
        }
    }
}