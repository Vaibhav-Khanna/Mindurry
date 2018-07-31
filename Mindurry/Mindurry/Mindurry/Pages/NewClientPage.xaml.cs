using Mindurry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClientPage : ContentPage
	{
		public NewClientPage ()
		{
			InitializeComponent ();
		}
        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem;

            if (item != null)
            {
                ((NewClientPageModel)BindingContext)?.PickPlaceCommand.Execute(item);
            }
                  ((ListView)sender).SelectedItem = null;
        }

        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((NewClientPageModel)BindingContext)?.FindPlaceCommand.Execute(e.NewTextValue);
        }
    }
}