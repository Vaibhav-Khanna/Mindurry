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
	public partial class ResidencesDetailsApartmentsPage : ContentPage
	{
		public ResidencesDetailsApartmentsPage ()
		{
			InitializeComponent ();
		}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ResidencesDetailsApartmentsPageModel)BindingContext)?.SearchCommand.Execute(e.NewTextValue);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }
    }
}