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
    public class ResidencesDetailsCellarsPageModel : BasePageModel
    {

        public ObservableCollection<CellarsListModel> Cellars { get; set; }

        private CellarsListModel selectedCellar;
        [PropertyChanged.DoNotNotify]
        public CellarsListModel SelectedCellar
        {
            get => selectedCellar;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsCellarPageModel>(value);
                selectedCellar = null;
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
        private List<CellarsListModel> OriginalCellars;

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadCellars();

            LoadFilters();

            ShowFilterCommand = new Command(ShowFilter);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ClearAllFilterCommand = new Command(ClearAllFilters);
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
                    OriginalCellars = Cellars.ToList();
                }

            }

        }
        public void LoadFilters()
        {
            StatesChecks = new ObservableCollection<CheckBoxItem>();
            // Type Checkbox Items
            foreach (var item in Cellars.DistinctBy(s => s.Cellar.CommandState).OrderBy(s => s.Cellar.CommandState).ToList())
            {
                CheckBoxItem check;
                StatesChecks.Add(check = new CheckBoxItem() { Content = item.Cellar.CommandState, DataType = CheckBoxContainerDataType.CommandState });
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
            var filter_list = new List<CellarsListModel>();

            filter_list = OriginalCellars;

            //Filtered for types
            var states_checked = StatesChecks.Where(r => r.IsChecked).ToList();
            if (states_checked.Any())
            {
                var temp_list = new List<CellarsListModel>();

                foreach (var item in states_checked)
                {
                    temp_list.AddRange(filter_list.Where(x => x.Cellar.CommandState == item.Content));
                }

                filter_list = temp_list;
            }
            Cellars = new ObservableCollection<CellarsListModel>(filter_list);
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
