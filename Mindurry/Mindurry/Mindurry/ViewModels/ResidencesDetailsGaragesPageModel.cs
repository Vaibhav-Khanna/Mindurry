using Mindurry.DataModels;
using Mindurry.Models;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsGaragesPageModel : BasePageModel
    {
        public ObservableCollection<GaragesListModel> Garages { get; set; }

        private GaragesListModel selectedGarage;
        [PropertyChanged.DoNotNotify]
        public GaragesListModel SelectedGarage
        {
            get => selectedGarage;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsGaragePageModel>(value);
                selectedGarage = null;
                RaisePropertyChanged();
            }
        }

        public bool IsStateListVisible { get; set; } = true;
        public string ArrowTwo
        {
            get => IsStateListVisible ? "" : "";
        }
        public bool IsFilterOn { get; set; } = false;
        public ICommand ShowFilterCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ClearAllFilterCommand { get; set; }
        public ObservableCollection<CheckBoxItem> StatesChecks { get; set; }


        private string residenceId;
        private List<GaragesListModel> OriginalGarages;

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadGarages();

            LoadFilters();

            ShowFilterCommand = new Command(ShowFilter);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ClearAllFilterCommand = new Command(ClearAllFilters);
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
                    OriginalGarages = Garages.ToList();
                }
            }
        }

        public void LoadFilters()
        {
            StatesChecks = new ObservableCollection<CheckBoxItem>();
            // Type Checkbox Items
            foreach (var item in Garages.DistinctBy(s => s.Garage.CommandState).OrderBy(s => s.Garage.CommandState).ToList())
            {
                CheckBoxItem check;
                StatesChecks.Add(check = new CheckBoxItem() { Content = item.Garage.CommandState, DataType = CheckBoxContainerDataType.CommandState});
                check.PropertyChanged += StateFilterChanged;
            }
        }

        void StateFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var state = sender as CheckBoxItem;

            Filter();
        }

        void Filter()
        {
            var filter_list = new List<GaragesListModel>();

            filter_list = OriginalGarages;

            //Filtered for types
            var states_checked = StatesChecks.Where(r => r.IsChecked).ToList();
            if (states_checked.Any())
            {
                var temp_list = new List<GaragesListModel>();

                foreach (var item in states_checked)
                {
                    temp_list.AddRange(filter_list.Where(x => x.Garage.CommandState == item.Content));
                }

                filter_list = temp_list;
            }
            Garages = new ObservableCollection<GaragesListModel>(filter_list);
        }
        void ClearAllFilters()
        {
            ClearStatesChecks();

        }
        void ClearStatesChecks()
        {
            foreach (var item in StatesChecks)
            {
                item.IsChecked = false;
            }
        }
        void ShowFilter()
        {
            IsFilterOn = !IsFilterOn;
        }

        void ChangeArrowTwo()
        {
            IsStateListVisible = !IsStateListVisible;
        }
    }
}
