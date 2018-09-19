using Mindurry.Models;
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
	public partial class SelectContactPage : ContentPage
	{
        SelectContactPageModel context;

        public SelectContactPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            context = BindingContext as SelectContactPageModel;
        }

        private void SearchPlace_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((SelectContactPageModel)BindingContext)?.SearchCommand.Execute(e.NewTextValue);
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if ((e.Item as ContactsListModel) == context.Contacts.LastOrDefault())
            {
                context.LoadMore.Execute(null);
            }
        }

    }
}