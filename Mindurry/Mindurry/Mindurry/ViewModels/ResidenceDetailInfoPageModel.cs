using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidenceDetailInfoPageModel : BasePageModel
    {
        public Residence Residence { get; set; }

        public ObservableCollection<DocumentMindurry> FilesList { get; set; }
    
        private string residenceId;

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadResidence();

            await LoadFiles();
        }
        public async Task LoadResidence()
        {
            Residence = await StoreManager.ResidenceStore.GetItemAsync(residenceId);
        }

        public async Task LoadFiles()
        {
            var files = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(residenceId, ReferenceKind.Residence.ToString().ToLower());
            if (files != null && files.Any())
            {
                files.OrderBy(x => x.Name);
                FilesList = new ObservableCollection<DocumentMindurry>(files);
            }
            else
            {
                FilesList = new ObservableCollection<DocumentMindurry>();
            }
            //Pull document from Azure Storage to  Locastorage if internet
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                await StoreManager.DocumentMindurryStore.PullLatest(residenceId, ReferenceKind.Residence.ToString().ToLower());
                // var str = await PclStorage.ReturnFolderPath(ReferenceKind.Apartment.ToString().ToLower());
            }
        }
        public Command DisplayPlanCommand => new Command<DocumentMindurry>(async (obj) =>
        {
            await CoreMethods.PushPageModel<PdfDisplayPageModel>(obj, true);


        });
    }
}
