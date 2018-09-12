using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class PdfDisplayPageModel : BasePageModel
    {
        public DocumentMindurry documentToDisplay;

        public Stream PdfDocumentStream { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Visibility { get; set; }
        public async override void Init(object initData)
        {
            base.Init(initData);

            documentToDisplay = (DocumentMindurry)initData;

            Title = documentToDisplay.Name;
            var docDownloaded = await PclStorage.LoadFileLocal(documentToDisplay.InternalName + ".pdf", documentToDisplay.ReferenceKind, documentToDisplay.ReferenceId);
            if (docDownloaded!= null)
            {
                PdfDocumentStream = new MemoryStream(docDownloaded);
                Visibility = true;
            }
            else
            {
                 Message = "Une erreur est survenue, Merci de réessayer.";
                Visibility = false;
            }

        }
        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);

        });
    }
}
