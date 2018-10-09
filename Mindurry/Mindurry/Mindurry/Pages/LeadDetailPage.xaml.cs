using Mindurry.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace Mindurry.Pages
{
    public partial class LeadDetailPage : ContentPage
    {
        
        public LeadDetailPage()
        {
            InitializeComponent();

            dtpicker.DateChanged += CalendarDatePicker_DateChanged;
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != null)
            {
                (BindingContext as LeadDetailPageModel).DateRem = args?.NewDate.Value.Date;
            }
        }
    }
}