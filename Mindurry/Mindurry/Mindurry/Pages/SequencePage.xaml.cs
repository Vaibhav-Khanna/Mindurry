using Mindurry.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.WebView.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	public partial class SequencePage : ContentPage
	{
		public SequencePage ()
		{
			InitializeComponent ();
            /*
            if (!FormsWebView.GlobalRegisteredHeaders.ContainsKey("ZUMO-API-VERSION"))
                FormsWebView.GlobalRegisteredHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            if (!FormsWebView.GlobalRegisteredHeaders.ContainsKey("X-ZUMO-AUTH"))
                FormsWebView.GlobalRegisteredHeaders.Add("X-ZUMO-AUTH", Settings.AuthToken);
                */
        }
        
    }
}