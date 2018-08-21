using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidenceDetailInfoPageModel : BasePageModel
    {
        public Residence Residence { get; set; }

        public ObservableCollection<DocumentMindurry> FilesList { get; set; }

        private DocumentMindurry selectedFile;
        public DocumentMindurry SelectedFile
        {
            get => selectedFile;
            set
            {
                if (value != null)
                   // CoreMethods.PushPageModel<ViewModels.ResidencesDetailsCellarPageModel>(value);
                selectedFile = null;
            }
        }

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
        }
    }
}
