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
	
	public partial class NewContactPage : ContentPage
	{
		public NewContactPage ()
		{
			InitializeComponent ();
		}
        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem;

            if (item != null)
            {
                ((NewContactPageModel)BindingContext)?.PickPlaceCommand.Execute(item);
            }
                  ((ListView)sender).SelectedItem = null;
        }

        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((NewContactPageModel)BindingContext)?.FindPlaceCommand.Execute(e.NewTextValue);
        }
    }
}