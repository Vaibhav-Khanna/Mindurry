using Mindurry.Helpers;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ApartmentDetailInfoPageModel : BasePageModel
    {
        public Apartment Apartment { get; set; }

        public string ContactName { get; set; }

        public ObservableCollection<Terrace> Terraces { get; set; }

        public ObservableCollection<Garden> Gardens { get; set; }

        public Stream PdfDocumentStream { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            Apartment = (Apartment)initData;

            if (!string.IsNullOrEmpty(Apartment.ContactId))
            {
                Contact Contact = await StoreManager.ContactStore.GetItemAsync(Apartment.ContactId);
                ContactName = Contact.Name;
            }

            // terraces list
            var terraces = await StoreManager.TerraceStore.GetTerracesByResidenceId(Apartment.ResidenceId, Apartment.Id);
            if (terraces != null && terraces.Any())
            {
                Terraces = new ObservableCollection<Terrace>(terraces);
            }
            // gardens list
            var gardens = await StoreManager.GardenStore.GetGardensByResidenceId(Apartment.ResidenceId, Apartment.Id);
            if (gardens != null && gardens.Any())
            {
                Gardens = new ObservableCollection<Garden>(gardens);
            }
            //Pull document from Azure Storage to  Locastorage if internet
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                await StoreManager.DocumentMindurryStore.PullLatest(Apartment.Id, ReferenceKind.Apartment.ToString().ToLower());
               // var str = await PclStorage.ReturnFolderPath(ReferenceKind.Apartment.ToString().ToLower());
            }
            //Documents
            var docs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(Apartment.Id, ReferenceKind.Apartment.ToString().ToLower());
            if (docs != null && docs.Any())
            {
                DocumentMindurry documentToDisplay;
                string fileName;

                var finals = docs.Where(x => x.DocumentType == DocumentType.Definitif.ToString().ToLower());
                if (finals != null && finals.Any())
                {
                    documentToDisplay = finals.First();
                    fileName = documentToDisplay.InternalName + "." + documentToDisplay.Extension;
                    var docDownloaded = await PclStorage.LoadFileLocal(fileName, ReferenceKind.Apartment.ToString().ToLower(), documentToDisplay.ReferenceId);
                    //  var docDownloaded = await StoreManager.DocumentMindurryStore.DownLoadDocument(documentToDisplay.Id);
                    PdfDocumentStream = new MemoryStream(docDownloaded);
                    
                }
                else
                {
                    var signs = docs.Where(x => x.DocumentType == DocumentType.Signe.ToString().ToLower());
                    if (signs != null && signs.Any())
                    {
                        documentToDisplay = signs.First();
                        fileName = documentToDisplay.InternalName + "." + documentToDisplay.Extension;
                        var docDownloaded = await PclStorage.LoadFileLocal(fileName, ReferenceKind.Apartment.ToString().ToLower(), documentToDisplay.ReferenceId);
                        //  var docDownloaded = await StoreManager.DocumentMindurryStore.DownLoadDocument(documentToDisplay.Id);
                        PdfDocumentStream = new MemoryStream(docDownloaded);
                    }
                    else
                    {
                        var initials = docs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower());
                        if (initials != null && initials.Any())
                        {
                            documentToDisplay = initials.First();
                            fileName = documentToDisplay.InternalName + "." + documentToDisplay.Extension;
                            var docDownloaded = await PclStorage.LoadFileLocal(fileName, ReferenceKind.Apartment.ToString().ToLower(), documentToDisplay.ReferenceId);
                            //    var docDownloaded = await StoreManager.DocumentMindurryStore.DownLoadDocument(documentToDisplay.Id);
                            PdfDocumentStream = new MemoryStream(docDownloaded);
                        }

                    }

                }

                
            }

        }
        public Command LinkToContactCommand => new Command(async () =>
        {
            var contact = await StoreManager.ContactStore.GetItemAsync(Apartment.ContactId);

            if (contact.Qualification == Qualification.Client.ToString())
            {
                await CoreMethods.PushPageModel<ClientsDetailsInfoPageModel>(Apartment.ContactId);
            }
            else
            {
                await CoreMethods.PushPageModel<LeadDetailPageModel>(Apartment.ContactId);
            }

        }
        );
    }
}