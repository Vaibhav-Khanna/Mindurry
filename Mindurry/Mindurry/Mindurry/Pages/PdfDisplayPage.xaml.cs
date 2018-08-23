using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mindurry.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfDisplayPage : ContentPage
	{
		public PdfDisplayPage ()
		{
			InitializeComponent ();
            pdfViewerControl.DocumentSaveInitiated += PdfViewerControl_DocumentSaveInitiated;
            
        }

        private async void PdfViewerControl_DocumentSaveInitiated(object sender, Syncfusion.SfPdfViewer.XForms.DocumentSaveInitiatedEventArgs args)
        {
            var name = ((PdfDisplayPageModel)BindingContext)?.documentToDisplay.Name + ".pdf";
            var folder = await DependencyService.Get<ISave>().Save(args.SaveStream as MemoryStream, name);

            var result = await DisplayAlert("Téléchargement", "Le téléchargement du document est terminé", "Ouvrir le répertoire", "Fermer");
            if (result)
            {
                await DependencyService.Get<ISave>().LaunchFolder(folder);
            }
        }
    }
}