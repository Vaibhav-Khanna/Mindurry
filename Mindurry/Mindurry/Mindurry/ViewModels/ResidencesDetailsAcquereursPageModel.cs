using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsAcquereursPageModel : BasePageModel
    {
        public ObservableCollection<AcquereursListModel> Acquereurs { get; set; }

        private AcquereursListModel selectedAcquereur;
        [PropertyChanged.DoNotNotify]
        public AcquereursListModel SelectedAcquereur
        {
            get => selectedAcquereur;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ClientsDetailsInfoPageModel>(value.Contact.Id);
                selectedAcquereur = null;
                RaisePropertyChanged();
            }
        }
     

        private string residenceId;

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadAcquereurs();

        }
        public async Task LoadAcquereurs()
        {
            Acquereurs = new ObservableCollection<AcquereursListModel>();
            //apartement
            var apartments = await StoreManager.ApartmentStore.GetApartmentsByResidenceId(residenceId);
            if (apartments != null && apartments.Any())
            { 
                var validApartments = apartments.Where(x => !String.IsNullOrEmpty(x.ContactId)).OrderBy(x => x.LotNumberArchitect);
                foreach (var item in validApartments)
                {
                    var contact = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                    var acquereurToInsert = new AcquereursListModel
                    {
                        LotNumberArchitect = item.LotNumberArchitect,
                        Type = item.Kind,
                        Contact = contact,
                        Stage = item.Stage,
                        Price = item.Price
                    };
                    Acquereurs.Add(acquereurToInsert);
                }
               
            }

            //parking
            var parkings = await StoreManager.GarageStore.GetItemsByResidenceId(residenceId);
            if (parkings != null && parkings.Any())
            {
                var validParkings = parkings.Where(x => !String.IsNullOrEmpty(x.ContactId)).OrderBy(x => x.LotNumberArchitect);
                foreach (var item in validParkings)
                {
                    var contact = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                    var acquereurToInsert = new AcquereursListModel
                    {
                        LotNumberArchitect = item.LotNumberArchitect,
                        Type = "Parking",
                        Contact = contact,
                        Stage = item.Stage,
                        Price = item.Price
                    };
                    Acquereurs.Add(acquereurToInsert);
                }

            }

            //cellar
            var cellars = await StoreManager.CellarStore.GetItemsByResidenceId(residenceId);
            if (cellars != null && cellars.Any())
            {
                var validCellars = cellars.Where(x => !String.IsNullOrEmpty(x.ContactId)).OrderBy(x => x.LotNumberArchitect);
                foreach (var item in validCellars)
                {
                    var contact = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                    var acquereurToInsert = new AcquereursListModel
                    {
                        LotNumberArchitect = item.LotNumberArchitect,
                        Type = "Cave",
                        Contact = contact,
                        Stage = item.Stage,
                        Price = item.Price
                    };
                    Acquereurs.Add(acquereurToInsert);
                }

            }
        }
    }
}
