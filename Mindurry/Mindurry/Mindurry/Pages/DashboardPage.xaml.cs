using Mindurry.ViewModels;
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
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();

            start_dtpicker.DateChanged += CalendarDatePicker_StartDate_DateChanged;
            end_dtpicker.DateChanged += CalendarDatePicker_EndDate_DateChanged;
        }

        private void CalendarDatePicker_EndDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != null && (args.NewDate != args.OldDate))
            {
               // (BindingContext as DashboardPageModel).EndDate = args?.NewDate.Value.Date;
               
                    ((DashboardPageModel)BindingContext)?.EndDateChangedCommand.Execute(args?.NewDate.Value.Date);
               
                

            }
        }
        private void CalendarDatePicker_StartDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != null)
            {
                //(BindingContext as DashboardPageModel).StartDate = args?.NewDate.Value.Date;
                ((DashboardPageModel)BindingContext)?.StartDateChangedCommand.Execute(args?.NewDate.Value.Date);

            }
        }

    }
}