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
	public partial class ClientsPage : ContentPage
	{
		public ClientsPage ()
		{
			InitializeComponent ();
		}
        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((ClientsPageModel)BindingContext)?.SearchCommand.Execute(e.NewTextValue);
        }
    }
}