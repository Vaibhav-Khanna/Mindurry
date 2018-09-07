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
    public class ResidencesDetailsCellarsPageModel : BasePageModel
    {

        public ObservableCollection<CellarsListModel> Cellars { get; set; }

        private CellarsListModel selectedCellar;
        public CellarsListModel SelectedCellar
        {
            get => selectedCellar;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsCellarPageModel>(value);
                selectedCellar = null;
            }
        }
        private string residenceId;

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadCellars();
        }

        public async Task LoadCellars()
        {
            Cellars = new ObservableCollection<CellarsListModel>();
            var cellars = await StoreManager.CellarStore.GetItemsByResidenceId(residenceId);
            if (cellars != null && cellars.Any())
            {
                foreach (var item in cellars)
                {
                    var cellarsListItem = new CellarsListModel
                    {
                        Cellar = item
                    };
                    if (!String.IsNullOrEmpty(item.ContactId))
                    {
                        var acquereur = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                        cellarsListItem.Client = acquereur.Name;
                    }
                    Cellars.Add(cellarsListItem);
                }

            }

        }
    }
}