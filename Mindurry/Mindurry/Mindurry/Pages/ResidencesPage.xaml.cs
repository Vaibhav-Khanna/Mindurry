using Mindurry.DataModels;
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
            var element = sender as Xamarin.Forms.Label;
            App.RequestTabbedPage(element.Text);
        }
    }
}