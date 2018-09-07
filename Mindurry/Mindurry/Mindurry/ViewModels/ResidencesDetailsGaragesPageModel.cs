using Mindurry.DataModels;
using Mindurry.Models;
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
    public class ResidencesDetailsGaragesPageModel : BasePageModel
    {
        public ObservableCollection<GaragesListModel> Garages { get; set; }

        private GaragesListModel selectedGarage;
        public GaragesListModel SelectedGarage
        {
            get => selectedGarage;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsGaragePageModel>(value);
                selectedGarage = null;
            }
        }
        private string residenceId;
        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;
          
            residenceId = data;

            await LoadGarages();
        }

        public async Task LoadGarages()
        {
            Garages = new ObservableCollection<GaragesListModel>();
            var garages = await StoreManager.GarageStore.GetItemsByResidenceId(residenceId);
            if (garages != null && garages.Any())
            {
               foreach (var item in garages)
                {
                    var garagesListItem = new GaragesListModel
                    {
                        Garage = item
                    };
                    if (!String.IsNullOrEmpty(item.ContactId))
                    {
                        var acquereur = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                        garagesListItem.Client = acquereur.Name;
                    }
                    Garages.Add(garagesListItem);
                }

            }

        }
    }
}
