using Mindurry.Models;
using Mindurry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	public partial class ClientsDetailsInfoPage : ContentPage
	{
		public ClientsDetailsInfoPage ()
		{
			InitializeComponent ();

            dtPicker.DateChanged += DtPicker_DateChanged;  
		}

        private void DtPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != null)
            {
                (BindingContext as ClientsDetailsInfoPageModel).DateReminder = args.NewDate.Value.Date;
            }
        }
    }
}