﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnexionPage : ContentPage
	{
		public ConnexionPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}