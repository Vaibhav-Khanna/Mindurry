using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ApartmentDetailInfoPageModel : BasePageModel
    {
        public Apartment Apartment { get; set; }

        public ObservableCollection<Terrace> Terraces { get; set; }

        public ObservableCollection<Garden> Gardens { get; set; }


        public async override void Init(object initData)
        {
            base.Init(initData);

            Apartment = (Apartment)initData;
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

        }
    }
}