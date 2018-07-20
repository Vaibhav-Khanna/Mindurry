using Mindurry.DataModels;
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
	public partial class ContactsPage : ContentPage
	{

        public ContactsPage ()
		{
			InitializeComponent ();
		}
        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((ContactsPageModel)BindingContext)?.SearchCommand.Execute(e.NewTextValue);
        }
    }
}