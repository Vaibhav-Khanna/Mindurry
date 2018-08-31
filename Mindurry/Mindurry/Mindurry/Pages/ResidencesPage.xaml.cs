using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
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
	public partial class ResidencesPage : ContentPage
	{
		public ResidencesPage ()
		{
			InitializeComponent ();
		}

        private async void headerTapped(object sender, EventArgs e)
        {
            var element = sender as Xamarin.Forms.StackLayout;
            var children = element.Children[0].BindingContext as IGrouping<Residence, ResidenceModel>;
            var key = children.Key;

            var page_1 = FreshPageModelResolver.ResolvePageModel<ResidenceDetailInfoPageModel>(key.Id);
            page_1.Title = "Informations";

            var page_2 = FreshPageModelResolver.ResolvePageModel<ResidencesDetailsApartmentsPageModel>(key.Id);
            page_2.Title = "Appartements";

            var page_3 = FreshPageModelResolver.ResolvePageModel<ResidencesDetailsGaragesPageModel>(key.Id);
            page_3.Title = "Garages";

            var page_4 = FreshPageModelResolver.ResolvePageModel<ResidencesDetailsCellarsPageModel>(key.Id);
            page_4.Title = "Caves";

            var page_5 = FreshPageModelResolver.ResolvePageModel<ResidencesDetailsAcquereursPageModel>(key.Id);
            page_5.Title = "Acquéreurs";

            var tabbed_page = new TabbedPage() { Title = key.Name };

            tabbed_page.Children.Add(page_1);
            tabbed_page.Children.Add(page_2);
            tabbed_page.Children.Add(page_3);
            tabbed_page.Children.Add(page_4);
            tabbed_page.Children.Add(page_5);

            await Navigation.PushAsync(tabbed_page);
        }
    }
}