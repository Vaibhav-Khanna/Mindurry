using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	public partial class ResidencesPage : ContentPage
	{
		public ResidencesPage ()
		{
			InitializeComponent ();
		}

        private void headerTapped(object sender, EventArgs e)
        {
            var model = BindingContext as ViewModels.ResidencesPageModel;
        }
    }
}