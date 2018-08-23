using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ApartmentDetailInfoPage : ContentPage
	{
		public ApartmentDetailInfoPage ()
		{
			InitializeComponent();
            pdfViewerControl.Toolbar.Enabled = false;

        }
	}
}