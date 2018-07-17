using Mindurry.Pages.Base;
using Mindurry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	public partial class AddressPickPage : BasePage
	{
		public AddressPickPage()
		{
			InitializeComponent();
		}

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem;

            if (item != null)
            {
                ((AddressPickPageModel)BindingContext)?.PickPlaceCommand.Execute(item);
            }
                  ((ListView)sender).SelectedItem = null;
        }

        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((AddressPickPageModel)BindingContext)?.FindPlaceCommand.Execute(e.NewTextValue);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    search.Focus();
                });
            }
            else
            {
                await System.Threading.Tasks.Task.Delay(1000);

                Device.BeginInvokeOnMainThread(() =>
                {
                    search.Focus();
                });
            }
        }
    }
}
